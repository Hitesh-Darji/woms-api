using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;

namespace WOMS.Domain.Repositories
{
    public interface IRouteRepository : IRepository<Route>
    {
        Task<IEnumerable<Route>> GetRoutesByDateAsync(DateTime date, CancellationToken cancellationToken = default);
        Task<IEnumerable<Route>> GetRoutesByTechnicianAsync(string technicianId, DateTime? date = null, CancellationToken cancellationToken = default);
        Task<Route?> GetRouteWithStopsAsync(Guid routeId, CancellationToken cancellationToken = default);
        Task<decimal> GetAverageEfficiencyAsync(DateTime date, CancellationToken cancellationToken = default);
        Task<decimal> GetTotalDistanceAsync(DateTime date, CancellationToken cancellationToken = default);
        Task<decimal> GetTotalTimeAsync(DateTime date, CancellationToken cancellationToken = default);
        Task<int> GetTotalStopsAsync(DateTime date, CancellationToken cancellationToken = default);
    }
}
