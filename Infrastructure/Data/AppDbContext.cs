using ControleMercadoria.Core.Models.Movements;
using ControleMercadoria.Core.Models.Products;
using ControleMercadoria.Core.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace ControleMercadoria.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options){}
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Movement> Movements { get; set; }
    }
}
