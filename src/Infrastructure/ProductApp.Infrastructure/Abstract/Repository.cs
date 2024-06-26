using ProductApp.Shared.Abstract;

namespace ProductApp.Infrastructure.Abstract;
public abstract class Repository<T> : IRepository<T>
        where T : class, IEntity
{
    private readonly ProductDBContext _productDBContext;

    public ProductDBContext DBContext => _productDBContext;

    protected Repository(ProductDBContext productDBContext)
    {
        _productDBContext = productDBContext;
    }

    public abstract Task Create(T entity);

    public abstract Task Delete(Guid id);

    public abstract IQueryable<T> GetAll();

    public abstract Task<IEnumerable<T>> GetAllAsync();

    public abstract Task<T> GetById(Guid id);

    public abstract Task Update(Guid id, T entity);

    public async Task SaveAsync()
    {
        await DBContext.SaveChangesAsync();
    }
}
