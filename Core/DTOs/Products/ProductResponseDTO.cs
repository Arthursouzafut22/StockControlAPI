namespace ControleMercadoria.Core.DTOs.Products
{
    public class ProductResponseDTO(
        long Id,
        string Name,
        string Category,
        string Description,
        decimal PriceCost,
        decimal SalePrice,
        int StockQuantity,
        long UserId
    );
}