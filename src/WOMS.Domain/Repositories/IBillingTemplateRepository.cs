using System.Threading;
using System.Threading.Tasks;
using WOMS.Domain.Entities;

namespace WOMS.Domain.Repositories
{
    public interface IBillingTemplateRepository : IRepository<BillingTemplate>
    {
        Task<BillingTemplate?> GetByIdWithFieldOrderAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<BillingTemplate>> GetByCustomerIdAsync(string customerId, CancellationToken cancellationToken = default);
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<bool> ExistsByNameForCustomerAsync(string name, string customerId, CancellationToken cancellationToken = default);
    }
}
