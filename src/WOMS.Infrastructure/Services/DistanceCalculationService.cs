using WOMS.Application.Interfaces;
using WOMS.Domain.Repositories;

namespace WOMS.Infrastructure.Services
{
    public class DistanceCalculationService : IDistanceCalculationService
    {
        private readonly IAddressRepository _addressRepository;

        public DistanceCalculationService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<decimal> CalculateDistanceAsync(string fromLocation, string toLocation, CancellationToken cancellationToken = default)
        {
            // If locations are the same, return 0
            if (string.Equals(fromLocation, toLocation, StringComparison.OrdinalIgnoreCase))
                return 0;

            // Try to get coordinates for both locations
            var fromCoords = await GetCoordinatesAsync(fromLocation, cancellationToken);
            var toCoords = await GetCoordinatesAsync(toLocation, cancellationToken);

            if (fromCoords.HasValue && toCoords.HasValue)
            {
                return await CalculateDistanceFromCoordinatesAsync(
                    fromCoords.Value.lat, fromCoords.Value.lng,
                    toCoords.Value.lat, toCoords.Value.lng);
            }

            // Fallback to city-based distance estimation
            return EstimateDistanceByCity(fromLocation, toLocation);
        }

        public async Task<decimal> CalculateDistanceFromCoordinatesAsync(double fromLat, double fromLng, double toLat, double toLng)
        {
            // Haversine formula for calculating distance between two points
            const double earthRadiusKm = 6371.0;

            var dLat = ToRadians(toLat - fromLat);
            var dLng = ToRadians(toLng - fromLng);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians(fromLat)) * Math.Cos(ToRadians(toLat)) *
                    Math.Sin(dLng / 2) * Math.Sin(dLng / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var distanceKm = earthRadiusKm * c;

            return (decimal)Math.Round(distanceKm, 2);
        }

        public async Task<(double lat, double lng)?> GetCoordinatesAsync(string location, CancellationToken cancellationToken = default)
        {
            // Try to find coordinates in Address table
            var addresses = await _addressRepository.GetByLocationAsync(location, cancellationToken);
            var addressWithCoords = addresses.FirstOrDefault(a => !string.IsNullOrEmpty(a.Coordinates));

            if (addressWithCoords?.Coordinates != null)
            {
                try
                {
                    var coords = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, double>>(addressWithCoords.Coordinates);
                    if (coords != null && coords.ContainsKey("lat") && coords.ContainsKey("lng"))
                    {
                        return (coords["lat"], coords["lng"]);
                    }
                }
                catch
                {
                    // Ignore JSON parsing errors
                }
            }

            return null;
        }

        private static double ToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

        private static decimal EstimateDistanceByCity(string fromLocation, string toLocation)
        {
            // Simple city-based distance estimation
            var cityDistances = new Dictionary<string, Dictionary<string, decimal>>
            {
                ["Downtown"] = new() { ["Uptown"] = 8.5m, ["Suburbs"] = 15.2m, ["Airport"] = 12.8m },
                ["Uptown"] = new() { ["Downtown"] = 8.5m, ["Suburbs"] = 6.7m, ["Airport"] = 18.3m },
                ["Suburbs"] = new() { ["Downtown"] = 15.2m, ["Uptown"] = 6.7m, ["Airport"] = 25.1m },
                ["Airport"] = new() { ["Downtown"] = 12.8m, ["Uptown"] = 18.3m, ["Suburbs"] = 25.1m }
            };

            if (cityDistances.ContainsKey(fromLocation) && 
                cityDistances[fromLocation].ContainsKey(toLocation))
            {
                return cityDistances[fromLocation][toLocation];
            }

            // Default distance for unknown locations
            return 5.5m;
        }
    }
}
