using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProductApp.Data;

namespace ProductApp.Infrastructure
{
    public class ProductDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Provider> Providers { get; set; }

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

            base.OnModelCreating(modelBuilder);
        }
    }
}
