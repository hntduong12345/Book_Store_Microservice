using Users.Application.DTOs.Requests.Users;
using Users.Application.DTOs.Responses.Users;
using Users.Application.Interfaces;
using Users.Domain.Entities;
using Users.Domain.Interfaces;
using Users.Application.Mappers;

namespace Users.Application.Services
{
    public class UserService(IUserRepository repository) : IUserService
    {
        public async Task<UserResponse?> GetUserByIdAsync(Guid id)
        {
            var user = await repository.GetByIdAsync(id, includeAddresses: true);
            return user?.ToResponse();
        }

        public async Task CreateUserAsync(CreateUserRequest request)
        {
            var user = new User
            {
                Id = Guid.NewGuid(), // In the future, Keycloak will provide this
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await repository.InsertAsync(user);
        }
    }
}
