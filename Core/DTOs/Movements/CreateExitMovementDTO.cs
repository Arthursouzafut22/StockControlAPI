using System.Text.Json.Serialization;

namespace ControleMercadoria.Core.DTOs.Movements
{
    public record CreateExitMovementDTO(
        [property: JsonPropertyName("produto_id")]
        long ProductId,

        [property: JsonPropertyName("quantidade")]
        int Amount,

        [property: JsonPropertyName("valor_unitario")]
        decimal UnitValue,

        [property: JsonPropertyName("observacao")]
        string? Observation

    );

}
