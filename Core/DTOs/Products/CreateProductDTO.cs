using System.ComponentModel.DataAnnotations;

namespace ControleMercadoria.Core.DTOs.Products
{
    public record CreateProductDTO(
    string Name,
    string Category,
    string Description,
    decimal PriceCost,
    decimal SalePrice,
    int StockQuantity
);

}
