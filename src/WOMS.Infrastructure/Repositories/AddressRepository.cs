using Microsoft.EntityFrameworkCore;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;
using WOMS.Infrastructure.Data;

namespace WOMS.Infrastructure.Repositories
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(WomsDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Address>> GetByLocationAsync(string location, CancellationToken cancellationToken = default)
        {
            return await _context.Addresses
                .AsNoTracking()
                .Where(a => (a.City.Contains(location) || a.Street1.Contains(location)) && !a.IsDeleted)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Address>> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            return await _context.Addresses
                .AsNoTracking()
                .Where(a => a.CustomerId == customerId && !a.IsDeleted)
                .ToListAsync(cancellationToken);
        }

        public async Task<Address?> GetPrimaryAddressAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            return await _context.Addresses
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.CustomerId == customerId && a.IsPrimary && !a.IsDeleted, cancellationToken);
        }
    }
}
