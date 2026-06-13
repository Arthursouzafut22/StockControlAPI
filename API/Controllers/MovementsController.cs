using ControleMercadoria.Application.Services.Movements;
using ControleMercadoria.Core.DTOs.Movements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ControleMercadoria.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovementsController : ControllerBase
    {
        private readonly IMovementService _service;
        public MovementsController(IMovementService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost("entrada")]
        public async Task<IActionResult> RegisterEntry([FromBody] CreateEntryMovementDTO dto)
        {
            var userId= long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var teste = await _service.Create(userId, dto);
            return Ok(teste);
        }
    }
}
