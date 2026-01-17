using Users.Domain.Entities;

namespace Users.Domain.Interfaces
{
    public interface IUserRepository
    {
        //Get functions
        Task<User?> GetByIdAsync(Guid id, bool includeAddresses);
        Task<User?> GetByEmailAsync(string email, bool includeAddresses);

        //Insert functions
        Task InsertAsync(User user);

        //Update functions
        Task UpdateAsync(User user);

        //Soft delete function
        Task ArchivedAsync(Guid id);
    }
}