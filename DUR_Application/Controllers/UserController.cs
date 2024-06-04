using Microsoft.AspNetCore.Mvc;
using DUR_Application.Services.Services_User.UserServicesController;
using DUR_Application.Services.Services_User.UserDtos.CreateUser;
using Microsoft.AspNetCore.Http.HttpResults;
using DUR_Application.Services.Services_User.UserDtos.LoginUser;

namespace DUR_Application.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController:ControllerBase
    {
        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpPost("registration")]
        public async Task<IActionResult> CreateAccount([FromBody] CreateUserDto create)
        {
            await _userServices.CreateUser(create);
            var createdResource = new { Good = 1};
            return Created(string.Empty, createdResource);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            var token = await _userServices.LoginUser(loginUserDto);

            return Ok(token);

        }
    }
}
