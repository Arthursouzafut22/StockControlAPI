using ControleMercadoria.Core.DTOs.Products;

namespace ControleMercadoria.Application.Services.Products
{
    public interface IProductService
    {
        //Task<IEnumerable<ProductResponseDTO>> GetAll();
        //Task<ProductResponseDTO> FindById(long id, long userIdToken);
        Task<ProductResponseDTO> Create(CreateProductDTO product, long userId);
        //Task<UpdateProductDTO> Update(long userIdToken, UpdateProductDTO product);
        //Task Delete(long id, long userIdToken);
    }
}

