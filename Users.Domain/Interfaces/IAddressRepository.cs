using Users.Domain.Entities;

namespace Users.Domain.Interfaces
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Address>> GetByUserIdAsync(Guid userId);
        Task<Address?> GetByIdAsync(int id);

        Task<int> AddAsync(Address address);
        Task UpdateAsync(Address address);
        Task DeleteAsync(int id);

        // Logic to ensure only one 'is_default' address exists
        Task SetDefaultAddressAsync(Guid userId, int addressId);
    }
}
