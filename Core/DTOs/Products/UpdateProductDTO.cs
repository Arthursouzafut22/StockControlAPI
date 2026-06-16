using System.Text.Json.Serialization;

namespace ControleMercadoria.Core.DTOs.Products
{
    public record UpdateProductDTO(
        [property: JsonPropertyName("nome")]
        string Name,

        [property: JsonPropertyName("categoria")]
        string Category,

        [property: JsonPropertyName("descricao")]
        string Description,

        [property: JsonPropertyName("preco_custo")]
        decimal PriceCost,

        [property: JsonPropertyName("preco_venda")]
        decimal SalePrice,

        [property: JsonPropertyName("quantidade_estoque")]
        int StockQuantity
    );
}