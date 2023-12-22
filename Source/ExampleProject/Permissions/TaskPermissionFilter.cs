using ExampleProject.Models;
using MicroPermissions;

namespace ExampleProject.Permissions;

public class TaskPermissionFilter : IPermissionFilter<MyPermissionContext, IQueryable<TaskEntity>>
{
    public Task FilterResourceAsync(MyPermissionContext context, PermissionFilterEventArgs<IQueryable<TaskEntity>> perm)
    {
        if (context.ShowAllTasks == true)
            perm.FilteredResource = perm.OriginalResource;
        else
            perm.FilteredResource = perm.OriginalResource.Where(x => x.Owner == context.UserName);


        return Task.CompletedTask;
    }
}
