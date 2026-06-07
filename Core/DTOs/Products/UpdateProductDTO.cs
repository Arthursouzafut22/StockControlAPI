namespace ControleMercadoria.Core.DTOs.Products
{
    public class UpdateProductDTO(
    string Name,
    string Category,
    string Description,
    decimal PriceCost,
    decimal SalePrice,
    int StockQuantity
);
}
