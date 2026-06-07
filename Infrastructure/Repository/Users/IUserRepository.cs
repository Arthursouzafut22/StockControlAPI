using ControleMercadoria.Infrastructure.Repository.Generic;
using UserEntity = ControleMercadoria.Core.Models.Users.User;

namespace ControleMercadoria.Infrastructure.Repository.Users
{
    public interface IUserRepository: IGenericRepository<UserEntity>
    {
        Task<UserEntity> FindByEmail(string email);
        Task<bool> EmailExists(string email);
    }
}
