using ExampleProject.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExampleProject.Pages;

public class SettingsModel : PageModel
{
    private readonly MyPermissionContext myPermissionContext;

    [BindProperty] public bool ShowAllTasks { get; set; }
    [BindProperty] public bool AllowCounter { get; set; }

    public SettingsModel(MyPermissionContext myPermissionContext)
    {
        this.myPermissionContext = myPermissionContext;
    }

    public void OnGet()
    {
        ShowAllTasks = myPermissionContext.ShowAllTasks;
        AllowCounter = myPermissionContext.AllowCounter;
    }

    public void OnPost()
    {
        myPermissionContext.ShowAllTasks = ShowAllTasks;
        myPermissionContext.AllowCounter = AllowCounter;
    }
}
