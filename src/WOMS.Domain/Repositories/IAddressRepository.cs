using WOMS.Domain.Entities;

namespace WOMS.Domain.Repositories
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<IEnumerable<Address>> GetByLocationAsync(string location, CancellationToken cancellationToken = default);
        Task<IEnumerable<Address>> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default);
        Task<Address?> GetPrimaryAddressAsync(Guid customerId, CancellationToken cancellationToken = default);
    }
}
