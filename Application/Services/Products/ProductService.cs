using ControleMercadoria.Core.DTOs.Products;
using ControleMercadoria.Core.Models.Products;
using ControleMercadoria.Infrastructure.Repository.Products;
using ControleMercadoria.Infrastructure.Repository.Users;

namespace ControleMercadoria.Application.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository, IUserRepository repositoryUser)
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

        public async Task<ProductResponseDTO> Update(long userId, long productId, UpdateProductDTO dto)
        {
            var existProduct = await _repository.FindById(productId);

            if (existProduct == null)
                throw new KeyNotFoundException("Produto não encontrado");

            if (existProduct.UserId != userId)
                throw new UnauthorizedAccessException("Você não tem permissão para editar este produto.");


            existProduct.Name = dto.Name;
            existProduct.Description = dto.Description;
            existProduct.Category = dto.Category;
            existProduct.PriceCost = dto.PriceCost;
            existProduct.SalePrice = dto.SalePrice;
            existProduct.StockQuantity = dto.StockQuantity;

            var update = await _repository.Update(productId, existProduct);

            return new ProductResponseDTO(
                update.Id,
                update.Name,
                update.Category,
                update.Description,
                update.PriceCost,
                update.SalePrice,
                update.StockQuantity,
                update.UserId
            );
        }
    }
}