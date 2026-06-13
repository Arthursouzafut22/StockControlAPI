using ControleMercadoria.Core.Enums;
using ControleMercadoria.Core.Models.Products;

namespace ControleMercadoria.Core.DTOs.Movements
{
    public record CreateEntryMovementDTO
    (
          long ProductId,
          MovementType Type,
          int Amount,
          decimal UnitValue,
          //decimal TotalValue,
          string? Observation
    );
}
