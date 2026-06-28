using ControleMercadoria.Application.Services.Movements;
using ControleMercadoria.Core.DTOs.Movements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ControleMercadoria.API.Controllers
{
    [ApiController]
    [Route("v1/movimentacoes")]
    public class MovementsController : ControllerBase
    {
        private readonly IMovementService _service;

        public MovementsController(IMovementService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllMovements()
        {
            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var movements = await _service.GetAll(userId);

            return Ok(movements);
        }

        [Authorize]
        [HttpGet("entradas")]
        public async Task<IActionResult> GetEntryMovements()
        {
            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var movements = await _service.GetEntryMovements(userId);

            return Ok(movements);
        }

        [Authorize]
        [HttpPost("entradas")]
        public async Task<IActionResult> RegisterEntry([FromBody] CreateEntryMovementDTO dto)
        {
            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var movement = await _service.CreateEntryMovement(userId, dto);

            return StatusCode(201, new { message = "Movimentação de entrada registrada!", Data = movement });
        }

        [Authorize]
        [HttpGet("saidas")]
        public async Task<IActionResult> GetExitMovements()
        {
            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var movements = await _service.GetExitMovements(userId);

            return Ok(movements);
        }

        [Authorize]
        [HttpPost("saidas")]
        public async Task<IActionResult> RegisterExit([FromBody] CreateExitMovementDTO dto)
        {
            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var movementExit = await _service.CreateExitMovement(userId, dto);

            return StatusCode(201, new { message = "Movimentação de saida registrada!", Data = movementExit });
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdMovements(long id)
        {
            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var movements = await _service.FindByIdMovements(id, userId);
            return Ok(movements);
        }

        [Authorize]
        [HttpPut("{id}/{productId}")]
        public async Task<IActionResult> UpdateMovement(long id, long productId, 
            [FromBody] UpdateMovementsDTO dto)
        {
            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _service.UpdateMovement(id, userId, productId, dto);
            return Ok();
        }
    }
}
