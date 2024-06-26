using Microsoft.EntityFrameworkCore;
using ProductApp.Data;
using ProductApp.Infrastructure.Abstract;

namespace ProductApp.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly ProductDBContext _context;

    public UserRepository(ProductDBContext productDBContext) : base(productDBContext)
    {
        _context = productDBContext ?? throw new ArgumentNullException(nameof(productDBContext));
    }

    public override async Task Create(User entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        entity.DateCreated = DateTime.UtcNow;
        await _context.Users.AddAsync(entity);
        await SaveAsync();
    }

    public override async Task Delete(Guid id)
    {
        var user = await _context.Users.FindAsync(id) ?? throw new KeyNotFoundException("User not found.");

        user.IsDeleted = true;
        _context.Users.Update(user);
        await SaveAsync();
    }

    public override IQueryable<User> GetAll()
        => _context.Users.AsNoTracking().Where(user => !user.IsDeleted);

    public override async Task<IEnumerable<User>> GetAllAsync()
        => await _context.Users.AsNoTracking().Where(user => !user.IsDeleted).ToListAsync();

    public override async Task<User> GetById(Guid id)
    {
        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        if (user == null || user.IsDeleted)
        {
            throw new KeyNotFoundException("User not found.");
        }
        return user;
    }

    public override async Task Update(Guid id, User entity)
    {
        var existingUser = await _context.Users.FindAsync(id) ?? throw new KeyNotFoundException("User not found.");

        existingUser.FirstName = entity.FirstName;
        existingUser.LastName = entity.LastName;
        existingUser.Status = entity.Status;
        existingUser.IsDeleted = entity.IsDeleted;

        _context.Users.Update(existingUser);
        await SaveAsync();
    }

    public virtual async Task<IEnumerable<User>> GetAllIncludingDeletedAsync()
        => await _context.Users.AsNoTracking().ToListAsync();

    public virtual async Task<IEnumerable<User>> GetAllDeletedAsync()
        => await _context.Users.AsNoTracking().Where(user => user.IsDeleted).ToListAsync();

    public virtual async Task<User> GetByNameAsync(string firstName, string lastName)
    {
        var user = await _context.Users.AsNoTracking()
            .FirstOrDefaultAsync(u => u.FirstName == firstName && u.LastName == lastName && !u.IsDeleted);

        return user ?? throw new KeyNotFoundException("User not found.");
    }
}
