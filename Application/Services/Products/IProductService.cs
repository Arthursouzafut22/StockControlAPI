using ControleMercadoria.Core.DTOs.Products;

namespace ControleMercadoria.Application.Services.Products
{
    public interface IProductService
    {
        Task<ProductResponseDTO> Create(CreateProductDTO product, long userId);
        Task<ProductResponseDTO> Update(long userIdToken, long productId, UpdateProductDTO product);
        Task<IEnumerable<ProductResponseDTO>> GetAll(long userId);
        Task<ProductResponseDTO> FindById(long id, long userIdToken);
        Task Delete(long id, long userIdToken);
    }
}

