using ProductApp.Data;
using ProductApp.Services.Responses;

namespace ProductApp.Services.Abstract;

public interface IUserService
{
    Task<UserResponse> CreateUserAsync(User user);
    Task<UserResponse> DeleteUserAsync(Guid id);
    Task<IEnumerable<UserResponse>> GetAllUsersAsync();
    Task<UserResponse> GetUserByIdAsync(Guid id);
    Task<UserResponse> UpdateUserAsync(Guid id, User user);
    Task<IEnumerable<UserResponse>> GetAllDeletedUsersAsync();
    Task<UserResponse> GetUserByNameAsync(string firstName, string lastName);
}
