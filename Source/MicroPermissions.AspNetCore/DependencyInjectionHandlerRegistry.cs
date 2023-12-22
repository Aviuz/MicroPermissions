using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace MicroPermissions.AspNetCore
{
    public class DependencyInjectionHandlerRegistry<TContext> : IMicroPermissionsRegistry<TContext>
    {
        private readonly IServiceProvider serviceProvider;

        public DependencyInjectionHandlerRegistry(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IEnumerable<IPermissionFilter<TContext, T>> ResolveFilter<T>()
        {
            return serviceProvider.GetServices<IPermissionFilter<TContext, T>>();
        }

        public IEnumerable<IPermissionHandler<TContext, T>> ResolveHandler<T>()
        {
            return serviceProvider.GetServices<IPermissionHandler<TContext, T>>();
        }
    }
}