using UserEntity = ControleMercadoria.Models.Users.User;

namespace ControleMercadoria.Services.User
{
    public interface IUserService
    {
        Task<UserEntity> FindById(int id);
        Task<IEnumerable<UserEntity>> GetAll();
        Task<UserEntity> Create(UserEntity user);
        Task<UserEntity> Update(int id, UserEntity user);
        Task Delete(int id);
        Task<UserEntity> FindByEmail(string email);
        Task<bool> EmailExists(string email);
    }
}
