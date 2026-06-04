using ControleMercadoria.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;
using ControleMercadoria.Services.Auth;

namespace ControleMercadoria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        public AuthController(IAuthService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
          var login =  await _service.Login(dto);
          return Ok(new { Acess_token = login });
        }

    }
}
