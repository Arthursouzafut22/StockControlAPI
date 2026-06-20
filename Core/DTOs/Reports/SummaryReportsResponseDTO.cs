using System.Text.Json.Serialization;

namespace ControleMercadoria.Core.DTOs.Reports
{
    public record SummaryReportsResponseDTO(
        [property: JsonPropertyName("total_gasto")]
        decimal TotalSpent,

        [property: JsonPropertyName("total_vendido")]
        decimal TotalSold,

        [property: JsonPropertyName("lucro")]
        decimal Profit,

        [property: JsonPropertyName("possui_lucro")]
        bool IsProfitable
    );
}
