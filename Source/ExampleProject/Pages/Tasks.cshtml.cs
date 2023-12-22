using ExampleProject.Models;
using ExampleProject.Permissions;
using MicroPermissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExampleProject.Pages;

public class TasksModel : PageModel
{
    private readonly TaskRepository repository;
    private readonly MyPermissionContext permissionContext;
    private readonly PermissionController<MyPermissionContext> permissions;

    public TasksModel(TaskRepository repository, MyPermissionContext permissionContext, PermissionController<MyPermissionContext> permissions)
    {
        this.repository = repository;
        this.permissionContext = permissionContext;
        this.permissions = permissions;
    }

    public IList<TaskEntity> TaskList { get; set; }
    [BindProperty] public string NewTaskText { get; set; }

    public async Task OnGetAsync()
    {
        var allowedTasks = await permissions.FilterAsync(repository.Tasks.AsQueryable());

        TaskList = allowedTasks.ToList();
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid == false)
            return Page();

        if (await permissions.IsGrantedAsync(new CreateTaskRequest() { Text = NewTaskText }) == false)
            return Forbid();

        var newTask = new TaskEntity
        {
            Id = repository.GetNextId(),
            Owner = permissionContext.UserName,
            DateTime = DateTime.UtcNow,
            Text = NewTaskText,
        };

        repository.Tasks.Add(newTask);

        return RedirectToPage("./Tasks");
    }
}
