using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace MicroPermissions.AspNetCore
{
    public class DependencyInjectionHandlerRegistry : IPermissionHandlersRegistry
    {
        private readonly IServiceProvider serviceProvider;

        public DependencyInjectionHandlerRegistry(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IEnumerable<IPermissionHandler<T>> Resolve<T>() where T : IPermissionRequest
        {
            return serviceProvider.GetServices<IPermissionHandler<T>>();
        }
    }
}