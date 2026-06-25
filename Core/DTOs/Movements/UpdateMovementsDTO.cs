using System.Text.Json.Serialization;

namespace ControleMercadoria.Core.DTOs.Movements
{
    public record UpdateMovementsDTO
        (
        [property: JsonPropertyName("quantidade")]
        int Amount,

        [property: JsonPropertyName("valor_unitario")]
        decimal UnitValue,

        [property: JsonPropertyName("observacao")]
        string? Observation
        );
}
