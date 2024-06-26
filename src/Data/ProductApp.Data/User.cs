using ProductApp.Shared.Abstract;
using System.ComponentModel.DataAnnotations;

namespace ProductApp.Data;

public enum UserStatus
{
    Active,
    Inactive
}

public class User : IEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }

    [Required]
    public DateTime DateCreated { get; set; }

    [Required]
    public UserStatus Status { get; set; }

    [Required]
    public bool IsDeleted { get; set; }
}
