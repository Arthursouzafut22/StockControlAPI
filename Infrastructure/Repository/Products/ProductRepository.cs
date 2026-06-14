using ControleMercadoria.Infrastructure.Repository.Generic;
using ControleMercadoria.Infrastructure.Data;
using ControleMercadoria.Core.Models.Products;

namespace ControleMercadoria.Infrastructure.Repository.Products
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }
    }
}
