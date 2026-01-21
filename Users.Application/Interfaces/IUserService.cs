using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.DTOs.Requests.Users;
using Users.Application.DTOs.Responses.Users;

namespace Users.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse?> GetUserByIdAsync(Guid id);
        Task<UserResponse?> GetUserByEmailAsync(string email);

        Task CreateUserAsync(CreateUserRequest request);
        Task UpdateUserAsync(Guid id, UpdateUserRequest request);
        Task ArchivedUserAsync(Guid id);
    }
}
