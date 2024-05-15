
using ProductApp.HttpAPI.Extensions;

namespace ProductApp.HttpAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication
            .CreateBuilder(args)
            .ConfigureBuilder();

        var app = builder
            .Build()
            .ConfigureApplication();

        app.Run();
    }
}
