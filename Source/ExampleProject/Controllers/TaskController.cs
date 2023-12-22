using ExampleProject.Models;
using ExampleProject.Permissions;
using MicroPermissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExampleProject.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private readonly TaskRepository repository;
    private readonly MyPermissionContext permissionContext;
    private readonly MicroPermissions.PermissionController<MyPermissionContext> permissions;

    public TaskController(TaskRepository repository, MyPermissionContext permissionContext, PermissionController<MyPermissionContext> permissions)
    {
        this.repository = repository;
        this.permissionContext = permissionContext;
        this.permissions = permissions;
    }

    [HttpPost]
    public async Task<ActionResult> PostAsync(CreateTaskRequest request)
    {
        if (await permissions.IsGrantedAsync(request) == false)
            return Forbid();

        var newTask = new TaskEntity
        {
            Id = repository.GetNextId(),
            Owner = permissionContext.UserName,
            DateTime = DateTime.UtcNow,
            Text = request.Text,
        };

        repository.Tasks.Add(newTask);

        return NoContent();
    }
}