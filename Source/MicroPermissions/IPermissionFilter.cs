using System.Threading.Tasks;

namespace MicroPermissions
{
    /// <summary>
    /// Core functionality of micropermissions system. It filters resources where user has access to.
    /// </summary>
    /// <typeparam name="TContext">Permission context (in most scenarios this should be user/permission info)</typeparam>
    /// <typeparam name="TRequest">Request that should be valided for access</typeparam>
    public interface IPermissionFilter<TContext, TResource>
    {
        Task FilterResourceAsync(TContext context, PermissionFilterEventArgs<TResource> perm);
    }
}
