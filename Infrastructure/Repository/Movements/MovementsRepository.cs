using ControleMercadoria.Infrastructure.Repository.Generic;
using ControleMercadoria.Core.Models.Movements;
using ControleMercadoria.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ControleMercadoria.Infrastructure.Repository.Movements
{
    public class MovementsRepository : GenericRepository<Movement>, IMovementsRepository
    {
        public MovementsRepository(AppDbContext context): base(context) { }

        public async Task<IEnumerable<Movement>> GetAllMovementsWithProduct()
        {
           return await _context.Movements.Include(x => x.Product).ToListAsync();
        }
    }
}
