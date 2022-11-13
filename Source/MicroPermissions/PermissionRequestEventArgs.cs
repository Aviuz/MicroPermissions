namespace MicroPermissions
{
    public class PermissionRequestEventArguments
    {
        private bool accessGranted = false;
        private bool accessDenied = false;

        public bool IsSuccess => accessGranted == true && accessDenied == false;

        public void GrantAccess() => accessGranted = true;

        public void DenyAccess() => accessDenied = true;
    }
}
