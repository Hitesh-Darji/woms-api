using Microsoft.EntityFrameworkCore;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;
using WOMS.Infrastructure.Data;

namespace WOMS.Infrastructure.Repositories
{
    public class BillingScheduleRepository : Repository<BillingSchedule>, IBillingScheduleRepository
    {
        public BillingScheduleRepository(WomsDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(b => b.Name == name && !b.IsDeleted, cancellationToken);
        }
    }
}


