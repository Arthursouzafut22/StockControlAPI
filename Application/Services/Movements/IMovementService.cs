using ControleMercadoria.Core.DTOs.Movements;

namespace ControleMercadoria.Application.Services.Movements
{
    public interface IMovementService
    {
        Task<IEnumerable<MovementsResponseDTO>> GetAll(long userId);
        Task<MovementsResponseDTO> CreateEntryMovement(long userId, CreateEntryMovementDTO dto);
        Task<IEnumerable<MovementsResponseDTO>> GetEntryMovements(long userId);
        Task<MovementsResponseDTO> CreateExitMovement(long userId, CreateExitMovementDTO dto);
        Task<IEnumerable<MovementsResponseDTO>> GetExitMovements(long userId);
        Task<IEnumerable<MovementsResponseDTO>> FindByIdMovements(long id, long userId);
        Task UpdateMovement(long id, long userId, long productId, UpdateMovementsDTO dto);
    }
}
