using System;
using System.Collections.Generic;

namespace MicroPermissions.MemoryRegistry
{
    public class MemoryMicroPermissionsRegistry<TContext> : IMicroPermissionsRegistry<TContext>
    {
        private TypedRegistry handlersRegistry = new TypedRegistry();
        private TypedRegistry filtersRegistry = new TypedRegistry();

        public MemoryMicroPermissionsRegistry() { }

        public void RegisterHandler<THandler, TRequest>() where THandler : class, IPermissionHandler<TContext, TRequest> where TRequest : IPermissionRequest
            => handlersRegistry.Register<TRequest, THandler>();

        public void RegisterHandler<THandler, TRequest>(Func<THandler> resolveFunction) where THandler : class, IPermissionHandler<TContext, TRequest> where TRequest : IPermissionRequest
            => handlersRegistry.Register<TRequest>(resolveFunction);

        public void RegisterFilter<TFilter, TResource>() where TFilter : class, IPermissionFilter<TContext, TResource>
            => filtersRegistry.Register<TResource, TFilter>();

        public void RegisterFilter<TFilter, TResource>(Func<TFilter> resolveFunction) where TFilter : class, IPermissionFilter<TContext, TResource>
            => filtersRegistry.Register<TResource>(resolveFunction);

        public IEnumerable<IPermissionHandler<TContext, TRequest>> ResolveHandler<TRequest>() where TRequest : IPermissionRequest
            => handlersRegistry.Resolve<TRequest, IPermissionHandler<TContext, TRequest>>();

        public IEnumerable<IPermissionFilter<TContext, TResource>> ResolveFilter<TResource>()
            => filtersRegistry.Resolve<TResource, IPermissionFilter<TContext, TResource>>();
    }
}
