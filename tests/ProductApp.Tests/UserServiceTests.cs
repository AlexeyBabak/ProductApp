using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ProductApp.Data;
using ProductApp.Infrastructure;
using ProductApp.Infrastructure.Repositories;
using ProductApp.Services.Services;

namespace ProductApp.Tests;

[TestFixture]
public class UserServiceTests
{
    private Mock<UserRepository> _userRepositoryMock;
    private Mock<ILogger<UserService>> _loggerMock;
    private UserService _userService;
    private ProductDBContext _dbContext;

    [SetUp]
    public void SetUp()
    {
        var dbContextOptions = new DbContextOptionsBuilder<ProductDBContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _dbContext = new ProductDBContext(dbContextOptions);
        _userRepositoryMock = new Mock<UserRepository>(_dbContext);
        _loggerMock = new Mock<ILogger<UserService>>();

        _userService = new UserService(_userRepositoryMock.Object, _loggerMock.Object);
    }

    [TearDown]
    public void TearDown()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }

    [Test]
    public async Task CreateUserAsync_ShouldCreateUser()
    {
        // Arrange
        var user = new User
        {
            FirstName = "John",
            LastName = "Doe",
            Status = UserStatus.Active
        };

        // Act
        var result = await _userService.CreateUserAsync(user);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.FirstName, Is.EqualTo(user.FirstName));
            Assert.That(result.LastName, Is.EqualTo(user.LastName));
        });
        _userRepositoryMock.Verify(repo => repo.Create(user), Times.Once);
    }

    [Test]
    public async Task GetUserByIdAsync_ShouldReturnUser()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
            FirstName = "John",
            LastName = "Doe",
            Status = UserStatus.Active
        };

        _userRepositoryMock.Setup(repo => repo.GetById(userId)).ReturnsAsync(user);

        // Act
        var result = await _userService.GetUserByIdAsync(userId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(userId));
        _userRepositoryMock.Verify(repo => repo.GetById(userId), Times.Once);
    }

    [Test]
    public async Task DeleteUserAsync_ShouldMarkUserAsDeleted()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
            FirstName = "John",
            LastName = "Doe",
            Status = UserStatus.Active
        };

        _userRepositoryMock.Setup(repo => repo.GetById(userId)).ReturnsAsync(user);
        _userRepositoryMock.Setup(repo => repo.Delete(userId)).Callback(() => user.IsDeleted = true);

        // Act
        var result = await _userService.DeleteUserAsync(userId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.IsDeleted, Is.True);
        _userRepositoryMock.Verify(repo => repo.Delete(userId), Times.Once);
    }

    [Test]
    public async Task GetAllUsersAsync_ShouldReturnUsers()
    {
        // Arrange
        var users = new List<User>
            {
                new() { FirstName = "John", LastName = "Doe", Status = UserStatus.Active },
                new() { FirstName = "Jane", LastName = "Smith", Status = UserStatus.Active }
            };

        _userRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);

        // Act
        var result = await _userService.GetAllUsersAsync();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(2));
        _userRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
    }

    [Test]
    public async Task UpdateUserAsync_ShouldUpdateUser()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
            FirstName = "John",
            LastName = "Doe",
            Status = UserStatus.Active
        };

        _userRepositoryMock.Setup(repo => repo.GetById(userId)).ReturnsAsync(user);
        _userRepositoryMock.Setup(repo => repo.Update(userId, user)).Callback(() =>
        {
            user.FirstName = "UpdatedFirstName";
            user.LastName = "UpdatedLastName";
        });

        // Act
        user.FirstName = "UpdatedFirstName";
        user.LastName = "UpdatedLastName";
        var result = await _userService.UpdateUserAsync(userId, user);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.FirstName, Is.EqualTo("UpdatedFirstName"));
            Assert.That(result.LastName, Is.EqualTo("UpdatedLastName"));
        });
        _userRepositoryMock.Verify(repo => repo.Update(userId, user), Times.Once);
    }

    [Test]
    public async Task GetAllDeletedUsersAsync_ShouldReturnDeletedUsers()
    {
        // Arrange
        var users = new List<User>
            {
                new() { FirstName = "John", LastName = "Doe", Status = UserStatus.Active, IsDeleted = true },
                new() { FirstName = "Jane", LastName = "Smith", Status = UserStatus.Active, IsDeleted = true }
            };

        _userRepositoryMock.Setup(repo => repo.GetAllDeletedAsync()).ReturnsAsync(users);

        // Act
        var result = await _userService.GetAllDeletedUsersAsync();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(2));
        _userRepositoryMock.Verify(repo => repo.GetAllDeletedAsync(), Times.Once);
    }

    [Test]
    public async Task GetUserByNameAsync_ShouldReturnUser()
    {
        // Arrange
        var user = new User
        {
            FirstName = "John",
            LastName = "Doe",
            Status = UserStatus.Active
        };

        _userRepositoryMock.Setup(repo => repo.GetByNameAsync("John", "Doe")).ReturnsAsync(user);

        // Act
        var result = await _userService.GetUserByNameAsync("John", "Doe");

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.FirstName, Is.EqualTo("John"));
            Assert.That(result.LastName, Is.EqualTo("Doe"));
        });
        _userRepositoryMock.Verify(repo => repo.GetByNameAsync("John", "Doe"), Times.Once);
    }
}
