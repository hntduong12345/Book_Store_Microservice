using Users.Domain.Entities;

namespace Users.Domain.Interfaces
{
    public interface IUserRepository
    {
        //Get functions
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);

        //Insert functions
        Task InsertAsync(User user);

        //Update functions
        Task UpdateAsync(User user);

        //Soft delete function
        Task ArchivedAsync(Guid id);
    }
}