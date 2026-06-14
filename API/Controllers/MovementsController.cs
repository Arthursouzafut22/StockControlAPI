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
        [HttpPost("entradas")]
        public async Task<IActionResult> RegisterEntry([FromBody] CreateEntryMovementDTO dto)
        {
            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var movement = await _service.CreateEntryMovement(userId, dto);

            return StatusCode(201, new { message = "Movimentação registrada!", Data = movement });
        }

        [Authorize]
        [HttpGet("entradas")]
        public async Task<IActionResult> GetEntryMovements()
        {
            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var movements = await _service.GetEntryMovements(userId);

            return Ok(movements);
        }
    }
}
