using BCrypt.Net;
using ControleMercadoria.Core.DTOs.Auth;
using ControleMercadoria.Infrastructure.Repository.RefreshTokens;
using ControleMercadoria.Infrastructure.Repository.Users;
using RefreshTokenEntity = ControleMercadoria.Core.Models.RefreshToken.RefreshToken;


namespace ControleMercadoria.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;
        private readonly ITokenService _token;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        private const int AccessTokenExpirationHours = 5;
        private const int RefreshTokenExpirationDays = 7;

        public AuthService(IUserRepository repository, ITokenService token, IRefreshTokenRepository refreshTokenRepository)
        {
            _repository = repository;
            _token = token;
            _refreshTokenRepository = refreshTokenRepository;
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

            var accessToken = _token.GenerateToken(user);
            var refreshTokenValue = _token.GenerateRefreshToken();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(RefreshTokenExpirationDays);

            var refreshTokenEntity = new RefreshTokenEntity
            {
                Token = refreshTokenValue,
                IsRevoked = false,
                ExpiresAt = refreshTokenExpiration,
                CreatedAt = DateTime.UtcNow,
                UserId = user.Id
            };


            await _refreshTokenRepository.Add(refreshTokenEntity);

            return new TokenResponseDTO(
                accessToken,
                DateTime.UtcNow.AddHours(AccessTokenExpirationHours),
                refreshTokenValue,
                refreshTokenExpiration
            );

        }

        public async Task<RefreshTokenResponseDTO> RefreshToken(RefreshTokenRequestDTO dto)
        {
            var existingToken = await _refreshTokenRepository.FindByToken(dto.RefreshToken);

            if (existingToken == null || existingToken.IsRevoked || existingToken.ExpiresAt < DateTime.UtcNow)
                throw new UnauthorizedAccessException("Refresh token inválido ou expirado.");

          
            await _refreshTokenRepository.Revoke(existingToken);

            var newAccessToken = _token.GenerateToken(existingToken.User!);
            var newRefreshTokenValue = _token.GenerateRefreshToken();
            var newRefreshTokenExpiration = DateTime.UtcNow.AddDays(RefreshTokenExpirationDays);

            var newRefreshTokenEntity = new RefreshTokenEntity
            {
                Token = newRefreshTokenValue,
                IsRevoked = false,
                ExpiresAt = newRefreshTokenExpiration,
                CreatedAt = DateTime.UtcNow,
                UserId = existingToken.UserId
            };

            await _refreshTokenRepository.Add(newRefreshTokenEntity);

            return new RefreshTokenResponseDTO(
                newAccessToken,
                newRefreshTokenValue,
                DateTime.UtcNow.AddHours(AccessTokenExpirationHours)
            );
        }
        public bool ToCheckSenha(string senha, string hashFromBank)
        {
            return BCrypt.Net.BCrypt.Verify(senha, hashFromBank);
        }
    }
}
