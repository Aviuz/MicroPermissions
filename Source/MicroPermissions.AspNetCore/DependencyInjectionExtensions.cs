using MicroPermissions;
using MicroPermissions.AspNetCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddMicroPermissions<TContext>(this IServiceCollection services)
        {
            services.AddTransient<IMicroPermissionsRegistry<TContext>, DependencyInjectionHandlerRegistry<TContext>>();
            services.AddTransient<PermissionController<TContext>>();

            return services;
        }
    }
}