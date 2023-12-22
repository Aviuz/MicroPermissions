using System.Threading.Tasks;

namespace MicroPermissions
{
    /// <summary>
    /// Core functionality of micropermissions system. It resolves access for specified request.
    /// </summary>
    /// <typeparam name="TContext">Permission context (in most scenarios this should be user/permission info)</typeparam>
    /// <typeparam name="TRequest">Request that should be valided for access</typeparam>
    public interface IPermissionHandler<TContext, TRequest>
    {
        Task HandleRequestAsync(TContext context, PermissionRequestEventArguments perm, TRequest request);
    }
}
