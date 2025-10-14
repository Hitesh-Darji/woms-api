using Microsoft.EntityFrameworkCore;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;
using WOMS.Infrastructure.Data;

namespace WOMS.Infrastructure.Repositories
{
    public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(WomsDbContext context) : base(context)
        {
        }

        public async Task<RefreshToken?> GetByTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.Refresh_Token == refreshToken, cancellationToken);
        }

        public async Task<RefreshToken> CreateAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
        {
            // Use the common AddAsync method from base repository
            return await AddAsync(refreshToken, cancellationToken);
        }

        // UpdateAsync and DeleteAsync are already available from base repository
        // No need to override them unless you need specific behavior

        public async Task DeleteByTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
        {
            var token = await _dbSet
                .FirstOrDefaultAsync(rt => rt.Refresh_Token == refreshToken, cancellationToken);
            
            if (token != null)
            {
                // Use the common DeleteAsync method from base repository
                await DeleteAsync(token, cancellationToken);
            }
        }
    }
}
