namespace ProductApp.Shared.Abstract;

public interface IRepository<T>
        where T : class, IEntity
{
    IQueryable<T> GetAll();

    Task<IEnumerable<T>> GetAllAsync();

    Task<T> GetById(Guid id);

    Task Create(T entity);

    Task Update(Guid id, T entity);

    Task Delete(Guid id);

    Task SaveAsync();
}
