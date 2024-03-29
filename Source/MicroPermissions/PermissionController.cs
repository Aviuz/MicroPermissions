﻿using System.Linq;
using System.Threading.Tasks;

namespace MicroPermissions
{
    public class PermissionController<TContext> : IPermissionController
    {
        private readonly IMicroPermissionsRegistry<TContext> registry;
        private readonly TContext context;
        private readonly PermissionControllerOptions options;

        public PermissionController(IMicroPermissionsRegistry<TContext> registry, TContext context, PermissionControllerOptions options)
        {
            this.registry = registry;
            this.context = context;
            this.options = options;
        }

        public async Task<bool> IsGrantedAsync<T>(T request)
        {
            var eventArgs = new PermissionRequestEventArguments();

            var handlers = registry.ResolveHandler<T>();

            if (options.ThrowIfUnhandled && handlers.Any() == false)
                throw new NotHandledPermissionRequestException(typeof(T).Name);

            foreach (var handler in handlers)
            {
                await handler.HandleRequestAsync(context, eventArgs, request);
            }


            return eventArgs.IsSuccess;
        }

        public async Task<T> FilterAsync<T>(T resource)
        {
            var eventArgs = new PermissionFilterEventArgs<T>(resource);

            foreach (var handler in registry.ResolveFilter<T>())
            {
                await handler.FilterResourceAsync(context, eventArgs);
            }

            if (options.ThrowIfUnhandled && eventArgs.ResourceChanged == false)
            {
                throw new NotHandledPermissionFilterException(typeof(T).Name);
            }

            if (eventArgs.AccessDenied == true)
            {
                return default;
            }

            return eventArgs.FilteredResource;
        }
    }
}
