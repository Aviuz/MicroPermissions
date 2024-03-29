# Micro Permissions [![.NET Build & Test](https://github.com/Aviuz/MicroPermissions/actions/workflows/dotnet-test.yml/badge.svg)](https://github.com/Aviuz/MicroPermissions/actions/workflows/dotnet-test.yml)
Minimalistic ASP.NET Policy/Resource Permissions permission system. It was inspired by [Policy-based authorization in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/security/authorization/policies). The difference here is it was designed to be used across multiple types of projects (service, aspnet core, common libraries), more modular and customaziable while keeping it simple and small.

# How to start
1. Install nuget
```
dotnet add package MicroPermissions.AspNetCore
```

2. Add permission context
```csharp
public class PermissionContext
{
    // You can use whatever properties you want, but here is example:
    public string UserName { get; private set; }
    public string[] Roles { get; private set; }

    public PermissionContext()
    {
        // you want to initialize UserName and Permissions depending on your project architecture
        // common case would be fetching from database user and his roles
    }
}
```

4. Add MicroPermissions to services
```csharp
builder.Services.AddScoped<PermissionContext>();
builder.Services.AddMicroPermissions<PermissionContext>();
```

5. Add permission handler or filter 
```csharp
// Typically for operations, creating/updating resources, viewing single entity etc.
public class BasicRequestPermissionHandler : IPermissionHandler<PermissionContext, BasicRequest>
{
    public Task HandleRequestAsync(PermissionContext context, PermissionRequestEventArguments perm, BasicRequest request)
    {
        if (context.Roles.Contains("basic"))
            perm.GrantAccess();

        return Task.CompletedTask;
    }
}

// Typically for returning collection of entities, hiding protected fields etc.
public class TaskPermissionFilter : IPermissionFilter<PermissionContext, IQueryable<Task>>
{
    public Task FilterResourceAsync(PermissionContext context, PermissionFilterEventArgs<IQueryable<Task>> perm)
    {
        perm.FilteredResource = perm.OriginalResource.Where(task => task.Owner == context.UserName);

        return Task.CompletedTask;
    }
}
```
You can define your own permission logic. For types used as resources there are no limitations.

6. Add handlers & filters to dependency injection
```csharp
builder.Services.AddTransient<IPermissionHandler<PermissionContext, BasicRequest>, BasicRequestPermissionHandler>();
builder.Services.AddTransient<IPermissionFilter<PermissionContext, IQueryable<Task>>, TaskPermissionFilter>();
```

7. Use in controller or any other service
```csharp
[ApiController]
public class TaskController : ControllerBase
{
    private readonly IPermissionController permissionController;

    public TaskController(IPermissionController permissionController)
    {
        this.permissionController = permissionController;
    }

    [HttpPost]
    public async Task<ActionResult> PostAsync(CreateTaskRequest request)
    {
        // ...

        if (await permissions.IsGrantedAsync(request) == false)
            return Forbid();

        // ...
    }
}
```

### For more information how to use library refer to [Example Project](/Source/ExampleProject)
