using System;

namespace MicroPermissions
{
    public class NotHandledPermissionRequestException : Exception
    {
        private const string MessageFormat =
@"Request of type '{0}' has got no permission handlers.
Please review your PermissionHandlers's.";

        public NotHandledPermissionRequestException(string requestName)
            : base(string.Format("", requestName)) { }
    }
}
