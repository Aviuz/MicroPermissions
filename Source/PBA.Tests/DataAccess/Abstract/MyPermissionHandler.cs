using System.Threading.Tasks;

namespace MicroPermissions.Tests.DataAccess.Abstract
{
    public class MyPermissionHandler : IPermissionHandler<DataAccessPermissionContext, MyPermissionRequest>
    {
        public Task HandleRequestAsync(DataAccessPermissionContext context, PermissionRequestEventArguments args, MyPermissionRequest request)
        {
            return Task.CompletedTask;
        }
    }
}
