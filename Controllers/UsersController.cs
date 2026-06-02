using ControleMercadoria.DTOs.User;
using ControleMercadoria.DTOs.Users;
using ControleMercadoria.Models.Users;
using ControleMercadoria.Services.User;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var user = await _service.FindById(id);

            if (user == null)
            {
                return NotFound($"Usuário com o ID {id} não foi encontrado.");
            }

            return Ok(user);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateUser(long id, [FromBody] UpdateUserDto dto)
        {
            var user = await _service.FindById(id);

            if (user == null)
            {
                return NotFound($"Usuário com o ID {id} não foi encontrado.");
            }

            var update = await _service.Update(id, dto);
            return CreatedAtAction(nameof(GetById), new { id = update.Id }, update);
        }
    }
}
