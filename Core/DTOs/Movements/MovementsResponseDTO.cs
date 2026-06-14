using ControleMercadoria.Core.Enums;

namespace ControleMercadoria.Core.DTOs.Movements
{
    public record MovementsResponseDTO
        (
          long Id,
          long ProductId,
          string ProductName,
          long UserId,
          MovementType Type,
          int Amount,
          decimal UnitValue,
          decimal TotalValue,
          string? Observation,
          DateTime CreatedAt
        );
}
