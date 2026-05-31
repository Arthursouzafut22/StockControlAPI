using ControleMercadoria.Repositoy.Generic;
using UserEntity = ControleMercadoria.Models.Users.User;

namespace ControleMercadoria.Repositoy.User
{
    public interface IUserRepository: IGenericRepository<UserEntity>
    {
        Task<UserEntity> FindByEmail(string email);
        Task<bool> EmailExists(string email);
    }
}
