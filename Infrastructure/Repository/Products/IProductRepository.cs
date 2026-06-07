using ControleMercadoria.Core.Models.Products;
using ControleMercadoria.Infrastructure.Repository.Generic;

namespace ControleMercadoria.Infrastructure.Repository.Products
{
    public interface IProductRepository : IGenericRepository<Product>
    {
    }
}
