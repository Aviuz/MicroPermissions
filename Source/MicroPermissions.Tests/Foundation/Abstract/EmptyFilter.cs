using System.Threading.Tasks;

namespace MicroPermissions.Tests.Foundation.Abstract
{
    class EmptyFilter : IPermissionFilter<PermissionContext, int>
    {
        public Task FilterResourceAsync(PermissionContext context, PermissionFilterEventArgs<int> args)
        {
            return Task.CompletedTask;
        }
    }
}
