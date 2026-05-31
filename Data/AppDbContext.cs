using ControleMercadoria.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleMercadoria.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
