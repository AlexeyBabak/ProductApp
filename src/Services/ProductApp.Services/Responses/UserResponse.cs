using ProductApp.Data;

namespace ProductApp.Services.Responses;

public enum UserStatus
{
    Active,
    Inactive
}

public class UserResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateCreated { get; set; }
    public UserStatus Status { get; set; }
    public bool IsDeleted { get; set; }

    public UserResponse(User user)
    {
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        DateCreated = user.DateCreated;
        Status = (UserStatus)user.Status;
        IsDeleted = user.IsDeleted;
    }
}
