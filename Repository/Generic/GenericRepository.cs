using System.Threading.Tasks;
using ControleMercadoria.Data;
using Microsoft.EntityFrameworkCore;

namespace ControleMercadoria.Repositoy.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T> Create(T item)
        {
            await _context.Set<T>().AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<T> Update(long id, T item)
        {
            var find = await FindById(id);

            if (find == null) return null;

            _context.Entry(find).State = EntityState.Detached;

            _context.Set<T>().Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> FindById(long id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            return entity ?? throw new KeyNotFoundException($"Entidade do tipo {typeof(T).Name} com id {id} não encontrada.");
        }

        public async Task Delete(long id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null) return;
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
