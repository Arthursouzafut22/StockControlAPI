using ControleMercadoria.Core.DTOs.Users;

namespace ControleMercadoria.Application.Services.Users
{
    public interface IUserService
    {
        Task<UserResponseDTO> FindById(long id, long userIdToken);
        Task<UserResponseDTO> Create(CreateUserDTO user);
        Task<UserResponseDTO> Update(long id, long userIdToken, UpdateUserDTO user);
        Task Delete(long id, long userIdToken);
    }
}
