using System.Threading.Tasks;

namespace MicroPermissions
{
    public abstract class PermissionController<TContext> where TContext : PermissionContext
    {
        private readonly IPermissionHandlersRegistry registry;

        public PermissionController(IPermissionHandlersRegistry registry)
        {
            this.registry = registry;
        }

        public async Task<bool> IsGrantedAsync<T>(TContext context, T request) where T : class, IPermissionRequest
        {
            foreach (var handler in registry.Resolve<T>())
            {
                await handler.HandleRequestAsync(context, request);
            }

            return context.Success;
        }
    }
}
