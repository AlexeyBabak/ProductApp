using ProductApp.Infrastructure;

namespace ProductApp.Migrations;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args)
            .ConfigureSQLServer("ProductApp.Migrations");

        var app = builder.Build();

        await app.RunAsync();
    }
}
