using ControleMercadoria.Core.Enums;
using System.Text.Json.Serialization;

namespace ControleMercadoria.Core.DTOs.Movements
{
    public record MovementsResponseDTO(
        [property: JsonPropertyName("id")]
        long Id,

        [property: JsonPropertyName("produto_id")]
        long ProductId,

        [property: JsonPropertyName("produto_nome")]
        string ProductName,

        [property: JsonPropertyName("usuario_id")]
        long UserId,

        [property: JsonPropertyName("tipo_movimentacao")]
        MovementType Type,

        [property: JsonPropertyName("quantidade")]
        int Amount,

        [property: JsonPropertyName("valor_unitario")]
        decimal UnitValue,

        [property: JsonPropertyName("valor_total")]
        decimal TotalValue,

        [property: JsonPropertyName("observacao")]
        string? Observation,

        [property: JsonPropertyName("data_registro")]
        DateTime CreatedAt
    );
}
