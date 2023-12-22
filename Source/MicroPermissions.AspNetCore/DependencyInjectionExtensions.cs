using MicroPermissions;
using MicroPermissions.AspNetCore;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddMicroPermissions<TContext>(this IServiceCollection services, PermissionControllerOptions options = null)
        {
            if (options != null)
                services.AddSingleton(options);
            else
                services.AddSingleton(new PermissionControllerOptions());

            services.AddTransient<IMicroPermissionsRegistry<TContext>, DependencyInjectionHandlerRegistry<TContext>>();
            services.AddTransient<PermissionController<TContext>>();

            return services;
        }

        public static IServiceCollection AddMicroPermissions<TContext>(this IServiceCollection services, Func<IServiceProvider, PermissionControllerOptions> createOptions)
        {
            services.AddTransient(createOptions);
            services.AddTransient<IMicroPermissionsRegistry<TContext>, DependencyInjectionHandlerRegistry<TContext>>();
            services.AddTransient<PermissionController<TContext>>();

            return services;
        }
    }
}