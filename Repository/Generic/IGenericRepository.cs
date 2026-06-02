namespace ControleMercadoria.Repositoy.Generic
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Create(T item);
        Task<T> Update(long id, T item);
        Task<IEnumerable<T>> GetAll();
        Task<T?> FindById(long id);
        Task Delete(long id);
    }
}
