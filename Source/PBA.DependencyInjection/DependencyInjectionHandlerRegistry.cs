using Microsoft.Extensions.DependencyInjection;
using PBA.Abstract;
using System;
using System.Collections.Generic;

namespace PBA.DependencyInjection
{
    public class DependencyInjectionHandlerRegistry : IPermissionHandlersRegistry
    {
        private readonly IServiceProvider serviceProvider;

        public DependencyInjectionHandlerRegistry(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IEnumerable<PermissionHandler<T>> Resolve<T>() where T : IPermissionRequest
        {
            return serviceProvider.GetServices<PermissionHandler<T>>();
        }
    }
}