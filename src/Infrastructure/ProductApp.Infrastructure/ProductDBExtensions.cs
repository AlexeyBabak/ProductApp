using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ProductApp.Infrastructure;

public static class ProductDBExtensions
{
    public static WebApplicationBuilder ConfigureSQLServer(this WebApplicationBuilder builder, string migrationAssemblyName)
    {
        var dbOptions = builder.Configuration.GetSection("ConnectionStrings").Get<DBContextSettings>();

        string connectionString = !string.IsNullOrEmpty(dbOptions.ProductDbConnectionString) ? dbOptions.ProductDbConnectionString : dbOptions.DefaultConnection;

        builder.Services.AddDbContext<ProductDBContext>(options =>
            options.UseSqlServer(connectionString, sqlOptions => sqlOptions.MigrationsAssembly(migrationAssemblyName)));

        return builder;
    }
}
