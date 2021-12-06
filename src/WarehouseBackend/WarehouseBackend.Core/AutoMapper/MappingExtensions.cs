using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace WarehouseBackend.Core.AutoMapper
{
    public static class MappingExtensions
	{
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
	}
}
