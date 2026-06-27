using ControleMercadoria.Application.Services.Users;
using ControleMercadoria.Core.DTOs.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ControleMercadoria.API.Controllers

{
    [ApiController]
    [Route("v1/usuarios")]
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

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, new
            {
                message = "Usuário cadastrado com sucesso!",
                data = user
            });
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
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(long id, [FromBody] UpdateUserDTO dto)
        {
            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var update = await _service.Update(id, userId, dto);

            return Ok(new
            {
                message = "Usuário atualizado com sucesso!",
                user = update
            });
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
