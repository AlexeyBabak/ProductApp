using System.ComponentModel.DataAnnotations;

namespace ProductApp.Data;

public class Provider : IProductEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MaxLength(200)]
    public string Address { get; set; }

    [Required]
    [MaxLength(100)]
    public string City { get; set; }

    [Required]
    [MaxLength(100)]
    public string Country { get; set; }

    [Range(1, 99999)]
    public int ZipCode { get; set; }

    public IEnumerable<Product> Products { get; set; }
}