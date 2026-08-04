using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Shared.Requests;
using Shared.Responses;
using UserService.Interfaces;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService service)
        {
            this.userService = service;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Shared.Requests.LoginRequest request)
        {
            LoginResponse response = await userService.LoginAsync(request);

            if (!response.Success)
                return Unauthorized(response);
            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Shared.Requests.RegisterRequest request)
        {
            var user = await userService.CreateUserAsync(request);
            return Created("", user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await userService.GetAllUsersAsync());
        }
    }
}