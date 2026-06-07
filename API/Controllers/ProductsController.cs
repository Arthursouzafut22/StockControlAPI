using ControleMercadoria.Application.Services.Products;
using ControleMercadoria.Core.DTOs.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ControleMercadoria.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO dto)
        {
            var userIdToken = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var createProduct = await _service.Create(dto, userIdToken);

            return StatusCode(201, new
            {
                message = "Produto cadastrado com sucesso.",
                Data = createProduct
            });
        }
    }
}
