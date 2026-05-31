namespace ControleMercadoria.Repositoy.Generic
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Create(T item);
        Task<T> Update(int id, T item);
        Task<IEnumerable<T>> GetAll();
        Task<T?> FindById(int id);
        Task Delete(int id);

    }
}
