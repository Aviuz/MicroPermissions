using ExampleProject.Models;
using MicroPermissions;

namespace ExampleProject.Permissions;

public class IncrementRequestPermissionHandler : IPermissionHandler<MyPermissionContext, IncrementRequest>
{
    public Task HandleRequestAsync(MyPermissionContext context, PermissionRequestEventArguments perm, IncrementRequest request)
    {
        if (context.AllowCounter == true)
            perm.GrantAccess();

        return Task.CompletedTask;
    }
}