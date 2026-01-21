using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.DTOs.Requests.Addresses;
using Users.Application.DTOs.Responses.Addresses;

namespace Users.Application.Interfaces
{
    public interface IAddressService
    {
        Task<AddressResponse?> GetAddressByIdAsync(int id);
        Task<IList<AddressResponse>> GetUserAddressesAsync(Guid userId);
        Task CreateAddressAsync(CreateAddressRequest request);
        Task UpdateAddressAsync(int id, UpdateAddressRequest request);
        Task SetDefaultAddressAsync(Guid userId, int addressId);
        Task DeleteAddressAsync(int id);
    }
}
