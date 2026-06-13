using ControleMercadoria.Infrastructure.Repository.Generic;
using ControleMercadoria.Core.Models.Movements;
using ControleMercadoria.Infrastructure.Data;

namespace ControleMercadoria.Infrastructure.Repository.Movements
{
    public class MovementsRepository : GenericRepository<Movement>, IMovementsRepository
    {
        public MovementsRepository(AppDbContext context): base(context) { }
    }
}
