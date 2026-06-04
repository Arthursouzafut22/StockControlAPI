using ControleMercadoria.Data;
using ControleMercadoria.Repositoy.Generic;
using ControleMercadoria.Repositoy.User;
using Microsoft.EntityFrameworkCore;
using UserEntity = ControleMercadoria.Models.Users.User;

namespace ControleMercadoria.Repository.User
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
