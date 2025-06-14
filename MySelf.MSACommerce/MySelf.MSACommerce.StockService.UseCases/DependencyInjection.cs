using Microsoft.Extensions.DependencyInjection;
using MySelf.MSACommerce.UseCases.Common;
using System.Reflection;

namespace MySelf.MSACommerce.StockService.UseCases
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUseCase(this IServiceCollection services)
        {
            services.AddUseCaseCommon(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
