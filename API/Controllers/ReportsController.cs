using ControleMercadoria.Application.Services.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ControleMercadoria.API.Controllers
{
    [ApiController]
    [Route("v1/relatorios")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportsService _service;

        public ReportsController(IReportsService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet("resumo")]
        public async Task<IActionResult> GetSummaryReport()
        {
            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var summary = await _service.GetSummaryReport(userId);
            return Ok(summary);
        }

        [Authorize]
        [HttpGet("estoque")]
        public async Task<IActionResult> GetInventoryReports()
        {
            var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var inventory = await _service.GetInventoryReports(userId);
            return Ok(inventory);
        }


    }
}
