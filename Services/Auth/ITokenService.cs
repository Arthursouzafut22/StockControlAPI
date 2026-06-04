using UserEntity = ControleMercadoria.Models.Users.User;

namespace ControleMercadoria.Services.Auth
{
    public interface ITokenService
    {
        string GenerateToken(UserEntity user);
    }
}
