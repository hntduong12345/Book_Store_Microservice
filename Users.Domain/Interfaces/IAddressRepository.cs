using Users.Domain.Entities;

namespace Users.Domain.Interfaces
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Address>> GetByUserIdAsync(Guid userId);
        Task<Address?> GetByIdAsync(int id);

        Task<int> InsertAsync(Address address);

        Task<int> UpdateAsync(Address address);
        // Logic to ensure only one 'is_default' address exists
        Task<int> SetDefaultAddressAsync(Guid userId, int addressId);
        Task<int> DeleteAsync(int id);
    }
}
