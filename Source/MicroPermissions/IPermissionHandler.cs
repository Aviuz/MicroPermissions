using System.Threading.Tasks;

namespace MicroPermissions
{
    public interface IPermissionHandler<TContext, TRequest> where TRequest : IPermissionRequest
    {
        Task HandleRequestAsync(TContext context, PermissionRequestEventArguments args, TRequest request);
    }
}
