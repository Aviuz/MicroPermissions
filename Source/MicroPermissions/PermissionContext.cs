namespace MicroPermissions
{
    public abstract class PermissionContext
    {
        public PermissionContext()
        {
            Success = false;
        }

        public bool Success { get; set; }

        public void GrantAccess() => Success = true;
    }
}
