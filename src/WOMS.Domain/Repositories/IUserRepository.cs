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
        Task<ApplicationUser?> GetByIdActiveAsync(string id, CancellationToken cancellationToken = default);
        Task SoftDeleteAsync(string id, string deletedBy, CancellationToken cancellationToken = default);
        Task<IEnumerable<ApplicationUser>> GetTechniciansAsync(string? status = null, string? location = null, CancellationToken cancellationToken = default);
    }
}
