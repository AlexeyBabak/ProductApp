using Microsoft.Extensions.DependencyInjection;
using ProductApp.Services.Abstract;
using ProductApp.Services.Services;

namespace ProductApp.Services
{
    public static class ProductServicesExtensions
    {
        public static IServiceCollection AddProductServices(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            return services;
        }
    }
}
