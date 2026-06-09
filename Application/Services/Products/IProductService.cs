using ControleMercadoria.Core.DTOs.Products;

namespace ControleMercadoria.Application.Services.Products
{
    public interface IProductService
    {
        //Task<IEnumerable<ProductResponseDTO>> GetAll();
        //Task<ProductResponseDTO> FindById(long id, long userIdToken);
        Task<ProductResponseDTO> Create(CreateProductDTO product, long userId);
        Task<ProductResponseDTO> Update(long userIdToken, long productId, UpdateProductDTO product);
        //Task Delete(long id, long userIdToken);
    }
}

