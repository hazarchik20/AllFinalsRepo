using FinalProjectASP_Net.Core.Abstractions.IServ;
using FinalProjectASP_Net.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
    public class UserController : Controller
    {

        public readonly IUserServices _usersService;

        public UserController(IUserServices usersService)
        {
            _usersService = usersService;
        }

        [HttpGet("HR")]
        public IActionResult GetHR([FromRoute] int limit = 10, [FromRoute] int offset = 0)
        {
            var result = _usersService.GetHR(10, 0);
            return Ok();
        }
        [HttpGet("Employee")]
        public IActionResult GetEmployee([FromRoute] int limit = 10, [FromRoute] int offset = 0)
        {
            var result = _usersService.GetEmployee(10, 0);
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult GetAll([FromRoute] int id)
        {
            var result = _usersService.GetById(id);
            return Ok();
        }
        [HttpGet]
        public IActionResult GetAll([FromRoute] int limit=10, [FromRoute] int offset=0)
        {
            var result = _usersService.GetAll(limit, offset);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _usersService.Delete(id);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserBase request)
        {
            await _usersService.Update( request);
            return Ok(request);
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
