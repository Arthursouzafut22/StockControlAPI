using ControleMercadoria.Infrastructure.Data;
using RefreshTokenEntity = ControleMercadoria.Core.Models.RefreshToken.RefreshToken;
using Microsoft.EntityFrameworkCore;


namespace ControleMercadoria.Infrastructure.Repository.RefreshTokens
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _context;
        public RefreshTokenRepository(AppDbContext context) => _context = context;

        public async Task<RefreshTokenEntity?> FindByToken(string token)
        {
            return await _context.RefreshTokens
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.Token == token);
        }

        public async Task Add(RefreshTokenEntity refreshToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken);
            await SaveChanges();
        }

        public async Task Revoke(RefreshTokenEntity refreshToken)
        {
            refreshToken.IsRevoked = true;
            await SaveChanges();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
