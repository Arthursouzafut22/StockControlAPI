using ControleMercadoria.Infrastructure.Data;
using ControleMercadoria.Infrastructure.Repository.Generic;
using Microsoft.EntityFrameworkCore;
using UserEntity = ControleMercadoria.Core.Models.Users.User;

namespace ControleMercadoria.Infrastructure.Repository.Users
{
    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }
        public async Task<UserEntity> FindByEmail(string email)
        {
            var findEmail = await _context.Set<UserEntity>().FirstOrDefaultAsync(item => item.Email == email);
            return findEmail;
        }
        public async Task<bool> EmailExists(string email) 
        {
            var existEmail = await _context.Set<UserEntity>().AnyAsync(item => item.Email == email);
            return existEmail;
        }
    }
}
