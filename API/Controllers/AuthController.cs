using Microsoft.AspNetCore.Mvc;
using ControleMercadoria.Core.DTOs.Auth;
using ControleMercadoria.Application.Services.Auth;

namespace ControleMercadoria.API.Controllers
{
    [ApiController]
    [Route("v1/autenticacao")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        public AuthController(IAuthService service) => _service = service;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var token = await _service.Login(dto);

            return Ok(new LoginResponseDTO(
                true, 
                "Login realizado com sucesso!", 
                token.Token,
                token.RefreshToken

             ));
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDTO dto)
        {
            var result = await _service.RefreshToken(dto);
            return Ok(result);
        }
    }
}
