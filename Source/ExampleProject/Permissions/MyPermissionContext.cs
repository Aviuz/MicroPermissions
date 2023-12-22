namespace ExampleProject.Permissions;

public class MyPermissionContext
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private static Dictionary<string, UserPermissions> permissions = new();

    public string? UserName => httpContextAccessor.HttpContext?.User?.Identity?.Name;

    public bool ShowAllTasks
    {
        get
        {
            return UserName != null && permissions[UserName].ShowAllTasks;
        }
        set
        {
            if (UserName != null)
                permissions[UserName].ShowAllTasks = value;
            Console.WriteLine($"UserName, ShowAllTasks {value}");
        }
    }

    public bool AllowCounter
    {
        get
        {
            return UserName != null && permissions[UserName].AllowCounter;
        }
        set
        {
            if (UserName != null)
                permissions[UserName].AllowCounter = value;
            Console.WriteLine($"UserName, AllowCounter {value}");
        }
    }

    public MyPermissionContext(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;

        if (UserName != null && permissions.ContainsKey(UserName) == false)
        {
            permissions.Add(UserName, new());
        }
    }

    public class UserPermissions
    {
        public bool ShowAllTasks { get; set; }
        public bool AllowCounter { get; set; }
    }
}
