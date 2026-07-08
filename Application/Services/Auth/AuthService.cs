using BCrypt.Net;
using ControleMercadoria.Core.DTOs.Auth;
using ControleMercadoria.Infrastructure.Repository.Users;

namespace ControleMercadoria.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;
        private readonly ITokenService _token;
        public AuthService(IUserRepository repository, ITokenService token)
        {
            _repository = repository;
            _token = token;
        }

        public async Task<TokenResponseDTO> Login(LoginDTO dto)
        {
            var user = await _repository.FindByEmail(dto.Email);

            if (user == null)
            {
                BCrypt.Net.BCrypt.Verify(dto.Password, "$2a$11$5Kfd64N25hXgY.V.3p4j9O");
                throw new UnauthorizedAccessException("E-mail ou senha inválidos.");
            }

            if (!ToCheckSenha(dto.Password, user.SenhaHash))
                throw new UnauthorizedAccessException("E-mail ou senha inválidos.");

            var token = _token.GenerateToken(user);

            return new TokenResponseDTO(token, DateTime.UtcNow.AddHours(5));
        }
        public bool ToCheckSenha(string senha, string hashFromBank)
        {
            return BCrypt.Net.BCrypt.Verify(senha, hashFromBank);
        }
    }
}
