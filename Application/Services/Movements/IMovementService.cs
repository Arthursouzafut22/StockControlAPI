using ControleMercadoria.Core.DTOs.Movements;

namespace ControleMercadoria.Application.Services.Movements
{
    public interface IMovementService
    {
        Task<MovementsResponseDTO> CreateEntryMovement(long userId, CreateEntryMovementDTO dto);
        Task<IEnumerable<MovementsResponseDTO>> GetEntryMovements(long userId);
        Task<MovementsResponseDTO> CreateExitMovement(long userId, CreateExitMovementDTO dto);
        Task<IEnumerable<MovementsResponseDTO>> GetExitMovements(long userId);
        Task DeleteMovement(long id, long userId);
    }
}
