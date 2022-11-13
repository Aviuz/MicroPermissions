using System.Threading.Tasks;

namespace MicroPermissions.Tests.Foundation.Abstract
{
    class EmptyNeutralHandler : IPermissionHandler<PermissionContext, EmptyRequest>
    {
        public Task HandleRequestAsync(PermissionContext context, PermissionRequestEventArguments args, EmptyRequest request)
        {
            return Task.CompletedTask;
        }
    }
}
