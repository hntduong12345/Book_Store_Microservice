using Users.Domain.Entities;

namespace Users.Domain.Interfaces
{
    public interface IUserRepository
    {
        //Get functions
        Task<User?> GetActiveByIdAsync(Guid id, bool includeAddresses);
        Task<User?> GetByEmailAsync(string email, bool includeAddresses);
        Task<User?> GetByIdAsync(Guid id);

        //Insert functions
        Task<int> InsertAsync(User user);

        //Update functions
        Task<int> UpdateAsync(User user);

        //Soft delete function
        Task<int> ArchivedAsync(Guid id);
    }
}