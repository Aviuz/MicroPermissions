using ExampleProject.Models;
using ExampleProject.Permissions;
using MicroPermissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExampleProject.Pages;

public class CounterModel : PageModel
{
    private readonly IPermissionController permissions;

    public int Count { get; set; }

    public CounterModel(IPermissionController permissions)
    {
        this.permissions = permissions;
    }

    public void OnGet()
    {
        Count = (int)(TempData["Count"] ?? 0);
    }

    public async Task<IActionResult> OnPost(IncrementRequest request)
    {
        if (await permissions.IsGrantedAsync(request) == false)
            return Forbid();

        Count = (int)(TempData["Count"] ?? 0) + request.Value;
        TempData["Count"] = Count;

        return RedirectToPage();
    }
}
