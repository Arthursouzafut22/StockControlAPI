using ControleMercadoria.Core.DTOs.Products;
using ControleMercadoria.Core.Models.Products;
using ControleMercadoria.Infrastructure.Repository.Products;

namespace ControleMercadoria.Application.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductResponseDTO> Create(CreateProductDTO dto, long userId)
        {
            var product = new Product
            {
                Name = dto.Name,
                Category = dto.Category,
                Description = dto.Description,
                PriceCost = dto.PriceCost,
                SalePrice = dto.SalePrice,
                StockQuantity = dto.StockQuantity,
                UserId = userId
            };

            var createdProduct = await _repository.Create(product);

            return new ProductResponseDTO(
                createdProduct.Id,
                createdProduct.Name,
                createdProduct.Category,
                createdProduct.Description,
                createdProduct.PriceCost,
                createdProduct.SalePrice,
                createdProduct.StockQuantity,
                createdProduct.UserId
            );
        }
    }
}