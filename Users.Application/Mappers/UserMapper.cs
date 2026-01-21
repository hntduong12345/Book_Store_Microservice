using Users.Application.DTOs.Requests.Users;
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

        public static User ToEntity(this CreateUserRequest request)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            };
        }

        public static void UpdateFromRequest(this User existingUser, UpdateUserRequest request)
        {
            // Only update if the request value is NOT null or empty
            existingUser.FirstName = !string.IsNullOrWhiteSpace(request.FirstName)
                ? request.FirstName
                : existingUser.FirstName;

            existingUser.LastName = !string.IsNullOrWhiteSpace(request.LastName)
                ? request.LastName
                : existingUser.LastName;

            // If Email is provided in update, update it, otherwise keep original
            existingUser.Email = !string.IsNullOrWhiteSpace(request.Email)
                ? request.Email
                : existingUser.Email;

            // Boolean protection: Only update if the request HAS a value (is not null)
            existingUser.IsActive = request.IsActive ?? existingUser.IsActive;
            existingUser.IsArchived = request.IsArchived ?? existingUser.IsArchived;

            existingUser.UpdatedAt = DateTime.UtcNow;
        }
    }
}
