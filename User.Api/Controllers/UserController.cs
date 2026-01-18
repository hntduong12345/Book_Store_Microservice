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
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await userService.GetUserByIdAsync(id);
            return user is not null ? Ok(user) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserRequest request)
        {
            await userService.CreateUserAsync(request);
            return Created();
        }
    }
}
