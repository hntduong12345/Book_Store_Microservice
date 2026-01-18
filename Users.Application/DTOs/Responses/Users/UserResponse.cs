using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.DTOs.Responses.Addresses;

namespace Users.Application.DTOs.Responses.Users
{
    public record UserResponse(
        Guid Id,
        string Email,
        string? FirstName,
        string? LastName,
        bool IsActive,
        List<AddressResponse>? Addresses
    );
}
