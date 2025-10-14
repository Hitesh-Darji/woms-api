using WOMS.Domain.Entities;

namespace WOMS.Domain.Repositories
{
    public interface IBillingScheduleRepository : IRepository<BillingSchedule>
    {
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}


