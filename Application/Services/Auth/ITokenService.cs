using UserEntity = ControleMercadoria.Core.Models.Users.User;

namespace ControleMercadoria.Application.Services.Auth
{
    public interface ITokenService
    {
        string GenerateToken(UserEntity user);
    }
}
