using PBA;
using PBA.Abstract;
using PBA.DependencyInjection;
using PBA.Handlers;
using PBA.Requests;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddPBA(this IServiceCollection services)
        {
            services.AddTransient<IPermissionHandlersRegistry, DependencyInjectionHandlerRegistry>();
            services.AddTransient<PermissionController>();
            
            services.AddTransient<PermissionHandler<PermissionRequest>, ServiceRequestHandler>();

            return services;
        }
    }
}