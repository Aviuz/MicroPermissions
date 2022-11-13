using System.Threading.Tasks;

namespace MicroPermissions
{
    public interface IPermissionFilter<TContext, TResource>
    {
        Task FilterResourceAsync(TContext context, PermissionFilterEventArgs<TResource> args);
    }
}
