using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductApp.Data;

public enum ProductState
{
    None,
    Created,
    Approved,
    Deployed,
    Available,
    NotAvailable
}

public class Product : IProductEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MaxLength(255)]
    public string Description { get; set; }

    [Column(TypeName = "decimal(8,2)")]
    public decimal Price { get; set; }

    [Required]
    public Guid ProviderId { get; set; }

    public int Quantity { get; set; }

    [Required]
    public ProductState State { get; set; }

    [Required]
    public Guid StateChangedBy { get; set; }

    [ForeignKey("ProviderId")]
    public Provider Provider { get; set; }
}