using Microsoft.EntityFrameworkCore;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;
using WOMS.Infrastructure.Data;

namespace WOMS.Infrastructure.Repositories
{
    public class RouteRepository : Repository<Route>, IRouteRepository
    {
        public RouteRepository(WomsDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Route>> GetRoutesByDateAsync(DateTime date, CancellationToken cancellationToken = default)
        {
            return await _context.Routes
                .Where(r => r.RouteDate.Date == date.Date && !r.IsDeleted)
                .Include(r => r.Driver)
                .Include(r => r.RouteStops)
                    .ThenInclude(rs => rs.WorkOrder)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Route>> GetRoutesByTechnicianAsync(string technicianId, DateTime? date = null, CancellationToken cancellationToken = default)
        {
            IQueryable<Route> query = _context.Routes
                .Where(r => r.DriverId == technicianId && !r.IsDeleted)
                .Include(r => r.Driver)
                .Include(r => r.RouteStops)
                    .ThenInclude(rs => rs.WorkOrder);

            if (date.HasValue)
            {
                query = query.Where(r => r.RouteDate.Date == date.Value.Date);
            }

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<Route?> GetRouteWithStopsAsync(Guid routeId, CancellationToken cancellationToken = default)
        {
            return await _context.Routes
                .Where(r => r.Id == routeId && !r.IsDeleted)
                .Include(r => r.Driver)
                .Include(r => r.RouteStops)
                    .ThenInclude(rs => rs.WorkOrder)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<decimal> GetAverageEfficiencyAsync(DateTime date, CancellationToken cancellationToken = default)
        {
            var routes = await _context.Routes
                .Where(r => r.RouteDate.Date == date.Date && !r.IsDeleted)
                .ToListAsync(cancellationToken);

            if (!routes.Any())
                return 0;

            return routes.Average(r => r.Efficiency);
        }

        public async Task<decimal> GetTotalDistanceAsync(DateTime date, CancellationToken cancellationToken = default)
        {
            return await _context.Routes
                .Where(r => r.RouteDate.Date == date.Date && !r.IsDeleted)
                .SumAsync(r => r.TotalDistance, cancellationToken);
        }

        public async Task<decimal> GetTotalTimeAsync(DateTime date, CancellationToken cancellationToken = default)
        {
            return await _context.Routes
                .Where(r => r.RouteDate.Date == date.Date && !r.IsDeleted)
                .SumAsync(r => r.TotalTime, cancellationToken);
        }

        public async Task<int> GetTotalStopsAsync(DateTime date, CancellationToken cancellationToken = default)
        {
            return await _context.Routes
                .Where(r => r.RouteDate.Date == date.Date && !r.IsDeleted)
                .SumAsync(r => r.TotalStops, cancellationToken);
        }
    }
}
