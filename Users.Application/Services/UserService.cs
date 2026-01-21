using Users.Application.DTOs.Requests.Users;
using Users.Application.DTOs.Responses.Users;
using Users.Application.Interfaces;
using Users.Domain.Entities;
using Users.Domain.Interfaces;
using Users.Application.Mappers;
using Users.Domain.Exceptions;

namespace Users.Application.Services
{
    public class UserService(IUserRepository repository) : IUserService
    {
        public async Task<UserResponse?> GetUserByIdAsync(Guid id)
        {
            var user = await repository.GetByIdAsync(id, includeAddresses: true);
            if(user == null) 
                throw new NotFoundException($"User with id {id} is not found");
            return user?.ToResponse();
        }

        public async Task<UserResponse?> GetUserByEmailAsync(string email)
        {
            var user = await repository.GetByEmailAsync(email, includeAddresses: true);
            if (user == null)
                throw new NotFoundException($"User with email {email} is not found");
            return user?.ToResponse();
        }

        public async Task CreateUserAsync(CreateUserRequest request)
        {
            var user = request.ToEntity();

            bool isCommitted = await repository.InsertAsync(user) > 0;
            if (!isCommitted)
                throw new ConflictException("Error occurs when insert user");
        }

        public async Task UpdateUserAsync(Guid id, UpdateUserRequest request)
        {
            var user = await repository.GetByIdAsync(id, includeAddresses: false);
            if (user == null)
                throw new NotFoundException($"User with id {id} is not found");

            user.UpdateFromRequest(request);

            bool isCommitted = await repository.InsertAsync(user) > 0;
            if (!isCommitted)
                throw new ConflictException("Error occurs when update user");
        }

        public async Task ArchivedUserAsync(Guid id)
        {
            var user = await repository.GetByIdAsync(id, includeAddresses: false);
            if (user == null)
                throw new NotFoundException($"User with id {id} is not found");

            bool isCommitted = await repository.ArchivedAsync(id) > 0;
            if (!isCommitted)
                throw new ConflictException("Error occurs when archived(soft-delete) user");
        }
    }
}
