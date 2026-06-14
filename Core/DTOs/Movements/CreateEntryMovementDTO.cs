namespace ControleMercadoria.Core.DTOs.Movements
{
    public record CreateEntryMovementDTO
    (
          long ProductId,
          int Amount,
          decimal UnitValue,
          string? Observation
    );
}
