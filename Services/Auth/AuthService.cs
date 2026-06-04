using ControleMercadoria.Data;
using ControleMercadoria.DTOs.Auth;
using ControleMercadoria.Repositoy.User;
using BCrypt.Net;

namespace ControleMercadoria.Services.Auth
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

        public async Task<string> Login(LoginDTO dto)
        {
            var user = await _repository.FindByEmail(dto.Email);

            if (user == null)
            {
                BCrypt.Net.BCrypt.Verify(dto.Senha, "$2a$11$5Kfd64N25hXgY.V.3p4j9O");
                throw new UnauthorizedAccessException("E-mail ou senha inválidos.");
            }

            if (!ToCheckSenha(dto.Senha, user.SenhaHash))
                throw new UnauthorizedAccessException("E-mail ou senha inválidos.");

            return _token.GenerateToken(user);
        }
        public bool ToCheckSenha(string senha, string hashFromBank)
        {
            return BCrypt.Net.BCrypt.Verify(senha, hashFromBank);
        }
    }
}
