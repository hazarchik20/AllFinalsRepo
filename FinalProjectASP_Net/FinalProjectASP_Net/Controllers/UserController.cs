using FinalProjectASP_Net.Core.Abstractions.IServ;
using FinalProjectASP_Net.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectASP_Net.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
    public class UserController: Controller
    {

        public readonly IUserServices _usersService;
       
        public UserController(IUserServices usersService)
        {
            _usersService = usersService;
        }

        [HttpGet("HR")]
        public IActionResult GetHR()
        {
            return Ok();
        }
        [HttpGet("Employee")]
        public IActionResult GetEmployee()
        {
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            return Ok();
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            var user = await _usersService.Register(request);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
        {
            var user = await _usersService.Login(request);
            return Ok(user);
        }


    }
}
