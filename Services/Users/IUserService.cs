using ControleMercadoria.DTOs.User;
using ControleMercadoria.DTOs.Users;

namespace ControleMercadoria.Services.User
{
    public interface IUserService
    {
        Task<UserResponseDTO> FindById(long id, long userIdToken);
        Task<IEnumerable<UserResponseDTO>> GetAll();
        Task<UserResponseDTO> Create(CreateUserDTO user);
        Task<UserResponseDTO> Update(long userIdToken, UpdateUserDTO user);
        Task Delete(long id, long userIdToken);
    }
}
