using ControleMercadoria.DTOs.User;
using ControleMercadoria.DTOs.Users;
using ControleMercadoria.Models.Users;
using ControleMercadoria.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO dto)
        {
            var user = await _service.Create(dto);

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var userIdToken = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var user = await _service.FindById(id, userIdToken);

            return Ok(user);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto dto)
        {
            var userIdToken = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var user = await _service.FindById(userIdToken, userIdToken);

            if (user == null)
            {
                return NotFound($"Usuário com o ID {userIdToken} não foi encontrado.");
            }

            var update = await _service.Update(userIdToken, dto);
            return CreatedAtAction(nameof(GetById), new { id = update.Id }, update);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _service.GetAll();
            return Ok(users);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var userIdToken = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _service.Delete(id, userIdToken);
            return NoContent();
        }
    }
}
