using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Users.Application.DTOs.Requests.Addresses;
using Users.Application.Interfaces;

namespace Users.Api.Controllers
{
    [Route("v1/api")]
    [ApiController]
    public class AddressController(IAddressService service) : ControllerBase
    {
        [HttpGet("addresses/{id:int}")]
        public async Task<IActionResult> GetAddressById([FromRoute]int id)
        {
            var address = await service.GetAddressByIdAsync(id);
            return Ok(address);
        }

        [HttpGet("users/{userId:guid}/addresses")]
        public async Task<IActionResult> GetUserAddresses([FromRoute]Guid userId)
        {
            var addresses = await service.GetUserAddressesAsync(userId);
            return Ok(addresses);
        }

        [HttpPost("addresses")]
        public async Task<IActionResult> CreateAddress([FromBody]CreateAddressRequest request)
        {
            await service.CreateAddressAsync(request);
            return Created();
        }

        [HttpPatch("addresses/{id:int}")]
        public async Task<IActionResult> UpdateAddress([FromRoute]int id, [FromBody]UpdateAddressRequest request)
        {
            await service.UpdateAddressAsync(id, request);
            return NoContent();
        }

        [HttpPatch("users/{userId:guid}/addresses/{id:int}")]
        public async Task<IActionResult> SetDefaultAddress(Guid userId, int id)
        {
            await service.SetDefaultAddressAsync(userId, id);
            return NoContent();
        }

        [HttpDelete("addresses/{id:int}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            await service.DeleteAddressAsync(id);
            return NoContent();
        }
    }
}
