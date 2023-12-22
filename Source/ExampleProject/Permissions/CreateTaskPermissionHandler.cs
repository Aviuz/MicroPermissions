using ExampleProject.Models;
using MicroPermissions;

namespace ExampleProject.Permissions;

public class CreateTaskPermissionHandler : IPermissionHandler<MyPermissionContext, CreateTaskRequest>
{
    public Task HandleRequestAsync(MyPermissionContext context, PermissionRequestEventArguments perm, CreateTaskRequest request)
    {
        perm.GrantAccess(); // allow for all users to create tasks

        return Task.CompletedTask;
    }
}
