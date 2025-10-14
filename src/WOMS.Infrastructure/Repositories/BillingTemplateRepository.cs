using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;
using WOMS.Infrastructure.Data;

namespace WOMS.Infrastructure.Repositories
{
    public class BillingTemplateRepository : Repository<BillingTemplate>, IBillingTemplateRepository
    {
        public BillingTemplateRepository(WomsDbContext context) : base(context)
        {
        }

        public async Task<BillingTemplate?> GetByIdWithFieldOrderAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(bt => bt.DynamicFields)
                .FirstOrDefaultAsync(bt => bt.Id == id && !bt.IsDeleted, cancellationToken);
        }

        public async Task<IEnumerable<BillingTemplate>> GetByCustomerIdAsync(string customerId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(bt => bt.CustomerId == customerId && !bt.IsDeleted)
                .OrderBy(bt => bt.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AnyAsync(bt => bt.Name == name && !bt.IsDeleted, cancellationToken);
        }

        public async Task<bool> ExistsByNameForCustomerAsync(string name, string customerId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AnyAsync(bt => bt.Name == name && bt.CustomerId == customerId && !bt.IsDeleted, cancellationToken);
        }
    }
}
