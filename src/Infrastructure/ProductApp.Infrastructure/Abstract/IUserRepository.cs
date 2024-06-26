using ProductApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApp.Infrastructure.Abstract;

public interface IUserRepository
{
    Task Create(User entity);
    Task Delete(Guid id);
    IQueryable<User> GetAll();
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> GetById(Guid id);
    Task Update(Guid id, User entity);
    Task<IEnumerable<User>> GetAllIncludingDeletedAsync();
    Task<IEnumerable<User>> GetAllDeletedAsync();
    Task<User> GetByNameAsync(string firstName, string lastName);
}
