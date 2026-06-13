using ControleMercadoria.Core.DTOs.Movements;

namespace ControleMercadoria.Application.Services.Movements
{
    public interface IMovementService
    {
        Task<MovementsResponseDTO> Create(long userId, CreateEntryMovementDTO dto);
    }
}
