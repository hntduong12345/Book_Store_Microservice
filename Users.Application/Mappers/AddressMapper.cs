using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.DTOs.Requests.Addresses;
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
                address.UserId,
                address.AddressLine1,
                address.AddressLine2,
                address.City,
                address.StateProvince,
                address.PostalCode,
                address.CountryCode,
                address.IsDefault,
                address.AddressType
            );
        }

        public static Address ToEntity(this CreateAddressRequest request) => new()
        {
            UserId = request.UserId,
            AddressLine1 = request.AddressLine1,
            AddressLine2 = request.AddressLine2,
            City = request.City,
            StateProvince = request.StateProvince,
            PostalCode = request.PostalCode,
            CountryCode = request.CountryCode,
            IsDefault = request.IsDefault,
            AddressType = request.AddressType
        };

        public static void UpdateFromRequest(this Address address, UpdateAddressRequest request)
        {
            address.AddressLine1 = request.AddressLine1 ?? address.AddressLine1;
            address.AddressLine2 = request.AddressLine2 ?? address.AddressLine2;
            address.City = request.City ?? address.City;
            address.StateProvince = request.StateProvince ?? address.StateProvince;
            address.PostalCode = request.PostalCode ?? address.PostalCode;
            address.CountryCode = request.CountryCode ?? address.CountryCode;
            address.IsDefault = request.IsDefault ?? address.IsDefault;
            address.AddressType = request.AddressType ?? address.AddressType;
        }
    }
}
