using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.DTOs.Responses.Addresses;
using Users.Domain.Entities;

namespace Users.Application.Mappers
{
    public static class AddressMapper
    {
        public static AddressResponse ToResponse(this Address address)
        {
            return new AddressResponse(
                address.Id,
                address.AddressLine1,
                address.City,
                address.CountryCode,
                address.IsDefault
            );
        }
    }
}
