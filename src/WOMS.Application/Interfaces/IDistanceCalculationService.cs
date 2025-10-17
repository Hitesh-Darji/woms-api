using WOMS.Domain.Entities;

namespace WOMS.Application.Interfaces
{
    public interface IDistanceCalculationService
    {
        Task<decimal> CalculateDistanceAsync(string fromLocation, string toLocation, CancellationToken cancellationToken = default);
        Task<decimal> CalculateDistanceFromCoordinatesAsync(double fromLat, double fromLng, double toLat, double toLng);
        Task<(double lat, double lng)?> GetCoordinatesAsync(string location, CancellationToken cancellationToken = default);
    }
}
