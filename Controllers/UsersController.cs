using Microsoft.AspNetCore.Mvc;
using ControleMercadoria.Services.User;
using UserEntity = ControleMercadoria.Models.Users.User;

namespace ControleMercadoria.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserEntity user)
        {
            var createUser = await _service.Create(user);
            return StatusCode(201 , createUser);      
        }
    }
}
