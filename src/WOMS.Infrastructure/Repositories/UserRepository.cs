using Microsoft.EntityFrameworkCore;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;
using WOMS.Infrastructure.Data;

namespace WOMS.Infrastructure.Repositories
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        public UserRepository(WomsDbContext context) : base(context)
        {
        }

        public async Task<ApplicationUser?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await GetFirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var user = await GetFirstOrDefaultAsync(u => u.Email == email, cancellationToken);
            return user != null;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllActiveAsync(CancellationToken cancellationToken = default)
        {
            return await FindAsync(u => !u.IsDeleted, cancellationToken);
        }

        public async Task<ApplicationUser?> GetByIdActiveAsync(string id, CancellationToken cancellationToken = default)
        {
            return await GetFirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted, cancellationToken);
        }

        public async Task SoftDeleteAsync(string id, string deletedBy, CancellationToken cancellationToken = default)
        {
            var user = await GetByIdAsync(Guid.Parse(id), cancellationToken);
            if (user != null)
            {
                user.IsDeleted = true;
                user.DeletedBy = deletedBy;
                user.DeletedOn = DateTime.UtcNow;
                await UpdateAsync(user, cancellationToken);
            }
        }
    }
}
