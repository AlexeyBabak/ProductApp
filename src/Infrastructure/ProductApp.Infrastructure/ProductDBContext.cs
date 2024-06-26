using Microsoft.EntityFrameworkCore;
using ProductApp.Data;

namespace ProductApp.Infrastructure;

public class ProductDBContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public DbSet<Provider> Providers { get; set; }

    public DbSet<User> Users { get; set; }

    public ProductDBContext(DbContextOptions<ProductDBContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasKey(p => p.Id)
            .HasName("PK_PRODUCT_ID");

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Provider)
            .WithMany(p => p.Products)
            .HasForeignKey(p => p.ProviderId);

        modelBuilder.Entity<Provider>()
            .HasKey(p => p.Id)
            .HasName("PK_PROVIDER_ID");

        modelBuilder.Entity<User>()
            .HasKey(p => p.Id)
            .HasName("PK_USER_ID");

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                FirstName = "John",
                LastName = "Doe",
                DateCreated = DateTime.UtcNow,
                Status = UserStatus.Active,
                IsDeleted = false
            }
        );

        base.OnModelCreating(modelBuilder);
    }
}
