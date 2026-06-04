using ControleMercadoria.DTOs.Auth;

namespace ControleMercadoria.Services.Auth
{
    public interface IAuthService
    {
        Task<string> Login(LoginDTO dto);
        bool ToCheckSenha(string senha, string hashFromBank);
    }
}
