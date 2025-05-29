

using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MySelf.MSACommerce.UseCases.Common.Behaviors;
using System.Reflection;

namespace MySelf.MSACommerce.UseCases.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUseCaseCommon(this IServiceCollection services,Assembly assembly)
        {
            services.AddAutoMapper(assembly);
            services.AddValidatorsFromAssembly(assembly);
            services.AddMediatR(
                config => {
                    config.RegisterServicesFromAssembly(assembly);
                    config.AddBehavior(typeof(IPipelineBehavior<,>),typeof(ValidationBehavior<,>));
                }
                );
            return services;    
        }
    }
}
