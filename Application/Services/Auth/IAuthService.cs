using ControleMercadoria.Core.DTOs.Auth;

namespace ControleMercadoria.Application.Services.Auth
{
    public interface IAuthService
    {
        Task<TokenResponseDTO> Login(LoginDTO dto);
        bool ToCheckSenha(string senha, string hashFromBank);
    }
}
