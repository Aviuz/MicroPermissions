using System.Threading.Tasks;

namespace MicroPermissions
{
    public abstract class PermissionHandler<TContext, TRequest> : IPermissionHandler<TRequest> where TContext : PermissionContext where TRequest : IPermissionRequest
    {
        public abstract Task HandleRequestAsync(TContext context, TRequest request);

        public Task HandleRequestAsync(PermissionContext context, TRequest request) => HandleRequestAsync(context as TContext, request);
    }
}
