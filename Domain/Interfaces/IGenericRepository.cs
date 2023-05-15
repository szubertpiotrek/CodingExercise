namespace Domain.Interfces
{
    public interface IGenericRepository<T>
    {
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> ListAsync();
        void Delete(T entity);
    }
}
