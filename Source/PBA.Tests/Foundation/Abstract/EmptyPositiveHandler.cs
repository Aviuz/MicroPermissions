using System.Threading.Tasks;

namespace MicroPermissions.Tests.Foundation.Abstract
{
    class EmptyPositiveHandler : IPermissionHandler<PermissionContext, EmptyRequest>
    {
        public Task HandleRequestAsync(PermissionContext context, PermissionRequestEventArguments args, EmptyRequest request)
        {
            args.GrantAccess();
            return Task.CompletedTask;
        }
    }
}
