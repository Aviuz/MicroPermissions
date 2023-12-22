using System.Threading.Tasks;

namespace MicroPermissions.Tests.Foundation.Abstract
{
    class IncrementFilter : IPermissionFilter<PermissionContext, int>
    {
        public Task FilterResourceAsync(PermissionContext context, PermissionFilterEventArgs<int> args)
        {
            args.FilteredResource = args.FilteredResource + 1;
            return Task.CompletedTask;
        }
    }
}
