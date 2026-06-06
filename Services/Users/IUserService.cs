using ControleMercadoria.DTOs.User;
using ControleMercadoria.DTOs.Users;

namespace ControleMercadoria.Services.User
{
    public interface IUserService
    {
        Task<UserResponseDto> FindById(long id, long userIdToken);
        Task<IEnumerable<UserResponseDto>> GetAll();
        Task<UserResponseDto> Create(CreateUserDTO user);
        Task<UserResponseDto> Update(long userIdToken, UpdateUserDto user);
        Task Delete(long id, long userIdToken);
    }
}
