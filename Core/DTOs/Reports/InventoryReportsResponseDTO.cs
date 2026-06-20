using System.Text.Json.Serialization;

namespace ControleMercadoria.Core.DTOs.Reports
{
    public record InventoryReportsResponseDTO(
        [property: JsonPropertyName("valor_total_estoque")]
        decimal TotalInventoryValue
    );
}