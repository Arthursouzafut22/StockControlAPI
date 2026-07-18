using RefreshTokenEntity = ControleMercadoria.Core.Models.RefreshToken.RefreshToken;

namespace ControleMercadoria.Infrastructure.Repository.RefreshTokens
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshTokenEntity?> FindByToken(string token);
        Task Add(RefreshTokenEntity refreshToken);
        Task Revoke(RefreshTokenEntity refreshToken);
        Task SaveChanges();
    }
}
