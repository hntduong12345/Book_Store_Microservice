using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Application.DTOs.Requests.Addresses
{
    public record UpdateAddressRequest
    (
        string? AddressLine1,
        string? AddressLine2,
        string? City,
        string? StateProvince,
        string? PostalCode,
        string? CountryCode,
        bool? IsDefault,
        string? AddressType
    );
}
