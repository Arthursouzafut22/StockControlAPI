using ControleMercadoria.Core.DTOs.Movements;

namespace ControleMercadoria.Application.Services.Movements
{
    public interface IMovementService
    {
        Task<MovementsResponseDTO> CreateEntryMovement(long userId, CreateEntryMovementDTO dto);
        Task<IEnumerable<MovementsResponseDTO>> GetEntryMovements(long userId);
    }
}
