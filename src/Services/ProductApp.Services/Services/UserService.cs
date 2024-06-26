using Microsoft.Extensions.Logging;
using ProductApp.Data;
using ProductApp.Infrastructure.Abstract;
using ProductApp.Services.Abstract;
using ProductApp.Services.Responses;

namespace ProductApp.Services.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserService> _logger;

    public UserService(IUserRepository userRepository, ILogger<UserService> logger)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<UserResponse> CreateUserAsync(User user)
    {
        try
        {
            await _userRepository.Create(user);
            _logger.LogInformation($"User created with ID: {user.Id}");
            return new UserResponse(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating user.");
            throw;
        }
    }

    public async Task<UserResponse> DeleteUserAsync(Guid id)
    {
        try
        {
            await _userRepository.Delete(id);
            var user = await _userRepository.GetById(id);
            _logger.LogInformation($"User marked as deleted with ID: {id}");
            return new UserResponse(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting user with ID: {id}");
            throw;
        }
    }

    public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
    {
        try
        {
            var users = await _userRepository.GetAllAsync();
            _logger.LogInformation("Retrieved all users.");
            return users.Select(user => new UserResponse(user));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving users.");
            throw;
        }
    }

    public async Task<UserResponse> GetUserByIdAsync(Guid id)
    {
        try
        {
            var user = await _userRepository.GetById(id);
            _logger.LogInformation($"User retrieved with ID: {id}");
            return new UserResponse(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving user with ID: {id}");
            throw;
        }
    }

    public async Task<UserResponse> UpdateUserAsync(Guid id, User user)
    {
        try
        {
            await _userRepository.Update(id, user);
            var updatedUser = await _userRepository.GetById(id);
            _logger.LogInformation($"User updated with ID: {id}");
            return new UserResponse(updatedUser);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating user with ID: {id}");
            throw;
        }
    }

    public async Task<IEnumerable<UserResponse>> GetAllDeletedUsersAsync()
    {
        try
        {
            var users = await _userRepository.GetAllDeletedAsync();
            _logger.LogInformation("Retrieved all deleted users.");
            return users.Select(user => new UserResponse(user));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving deleted users.");
            throw;
        }
    }

    public async Task<UserResponse> GetUserByNameAsync(string firstName, string lastName)
    {
        try
        {
            var user = await _userRepository.GetByNameAsync(firstName, lastName);
            _logger.LogInformation($"User retrieved with name: {firstName} {lastName}");
            return new UserResponse(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving user with name: {firstName} {lastName}");
            throw;
        }
    }
}
