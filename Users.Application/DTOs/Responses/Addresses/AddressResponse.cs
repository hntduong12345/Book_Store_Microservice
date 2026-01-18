using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Application.DTOs.Responses.Addresses
{
    public record AddressResponse(
        int Id,
        string AddressLine1,
        string City,
        string CountryCode,
        bool IsDefault
    );
}
