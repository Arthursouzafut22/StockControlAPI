using ControleMercadoria.DTOs.User;
using ControleMercadoria.DTOs.Users;

namespace ControleMercadoria.Services.User
{
    public interface IUserService
    {
        Task<UserResponseDto> FindById(long id);
        Task<IEnumerable<UserResponseDto>> GetAll();
        Task<UserResponseDto> Create(CreateUserDTO user);
        Task<UserResponseDto> Update(long id, UpdateUserDto user);
        Task Delete(long id);
    }
}
