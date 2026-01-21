using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Users.Application.DTOs.Requests.Users;
using Users.Application.Interfaces;

namespace Users.Api.Controllers
{
    [Route("v1/api/users")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById([FromRoute]Guid id)
        {
            var user = await userService.GetUserByIdAsync(id);
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserByEmail([FromQuery]string email)
        {
            var user = await userService.GetUserByEmailAsync(email);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserRequest request)
        {
            await userService.CreateUserAsync(request);
            return Created();
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserRequest request)
        {
            await userService.UpdateUserAsync(id, request);
            return NoContent();
        }

        [HttpPatch("{id:guid}/archived")]
        public async Task<IActionResult> ArchivedUser([FromRoute] Guid id)
        {
            await userService.ArchivedUserAsync(id);
            return NoContent();
        }
    }
}
