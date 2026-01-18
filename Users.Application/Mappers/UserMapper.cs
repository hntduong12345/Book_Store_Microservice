using Users.Application.DTOs.Responses.Users;
using Users.Domain.Entities;

namespace Users.Application.Mappers
{
    public static class UserMapper
    {
        public static UserResponse ToResponse(this User user)
        {
            return new UserResponse(
                user.Id,
                user.Email,
                user.FirstName,
                user.LastName,
                user.IsActive,
                user.Addresses.Select(a => a.ToResponse()).ToList()
            );
        }
    }
}
