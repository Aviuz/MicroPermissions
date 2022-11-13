using MicroPermissions;
using MicroPermissions.AspNetCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddMicroPermissions<TContext, TPermissionHandler>(this IServiceCollection services) where TContext : PermissionContext where TPermissionHandler : PermissionController<TContext>
        {
            services.AddTransient<IPermissionHandlersRegistry, DependencyInjectionHandlerRegistry>();
            services.AddScoped<TContext>();
            services.AddScoped<TPermissionHandler>();

            return services;
        }
    }
}