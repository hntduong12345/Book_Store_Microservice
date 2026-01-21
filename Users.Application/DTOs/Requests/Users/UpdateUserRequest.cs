using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Application.DTOs.Requests.Users
{
    public record UpdateUserRequest
    (
        string Email,
        string? FirstName,
        string? LastName,
        bool? IsActive,
        bool? IsArchived
    );
}
