using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.DTOs.Requests.Addresses;
using Users.Application.DTOs.Responses.Addresses;
using Users.Application.Interfaces;
using Users.Application.Mappers;
using Users.Domain.Entities;
using Users.Domain.Exceptions;
using Users.Domain.Interfaces;

namespace Users.Application.Services
{
    public class AddressService(IAddressRepository repository) : IAddressService
    {
        public async Task<AddressResponse?> GetAddressByIdAsync(int id)
        {
            var address = await repository.GetByIdAsync(id);
            return address?.ToResponse();
        }

        public async Task<IList<AddressResponse>> GetUserAddressesAsync(Guid userId)
        {
            var addresses = await repository.GetByUserIdAsync(userId);
            return addresses.Select(a => a.ToResponse()).ToList();
        }

        public async Task CreateAddressAsync(CreateAddressRequest request)
        {
            var entity = request.ToEntity();
            bool isCommitted = await repository.InsertAsync(entity) > 0;
            if (!isCommitted)
                throw new ConflictException("Error occurs when create new address");
        }

        public async Task UpdateAddressAsync(int id, UpdateAddressRequest request)
        {
            var address = await repository.GetByIdAsync(id)
            ?? throw new NotFoundException($"Address with id {id} not found");

            address.UpdateFromRequest(request);
            bool isCommitted = await repository.UpdateAsync(address) > 0;
            if (!isCommitted)
                throw new ConflictException("Error occurs when update address");
        }

        public async Task SetDefaultAddress(Guid userId, int addressId)
        {
            bool isCommitted = await repository.SetDefaultAddressAsync(userId, addressId) > 0;
            if (!isCommitted)
                throw new ConflictException("Error occurs when update default address");
        }

        public async Task DeleteAddressAsync(int id)
        {
            var address = await repository.GetByIdAsync(id)
            ?? throw new NotFoundException($"Address with id {id} not found");

            // --- Business Rules for Deletion ---
            // 1. Cannot delete if it is the default address
            if (address.IsDefault)
            {
                throw new InvalidOperationException("Cannot delete the default address. Please set another address as default first.");
            }

            // 2. Cannot delete if it is the only address
            var allUserAddresses = await repository.GetByUserIdAsync(address.UserId);
            if (allUserAddresses.Count() <= 1)
            {
                throw new InvalidOperationException("Cannot delete the only address associated with this user.");
            }

            bool isCommitted = await repository.DeleteAsync(id) > 0;
            if (!isCommitted)
                throw new ConflictException("Error occurs when delete address");
        }
    }
}
