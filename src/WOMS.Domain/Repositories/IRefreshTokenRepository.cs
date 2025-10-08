using WOMS.Domain.Entities;

namespace WOMS.Domain.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetByTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
        Task<RefreshToken> CreateAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);
        Task UpdateAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);
        Task DeleteAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);
        Task DeleteByTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
    }
}
