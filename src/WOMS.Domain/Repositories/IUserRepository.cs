using System;
using System.Threading;
using System.Threading.Tasks;
using WOMS.Domain.Entities;

namespace WOMS.Domain.Repositories
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<ApplicationUser?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<IEnumerable<ApplicationUser>> GetAllActiveAsync(CancellationToken cancellationToken = default);
        Task<ApplicationUser?> GetByIdActiveAsync(Guid id, CancellationToken cancellationToken = default);
        Task SoftDeleteAsync(Guid id, Guid deletedBy, CancellationToken cancellationToken = default);
    }
}
