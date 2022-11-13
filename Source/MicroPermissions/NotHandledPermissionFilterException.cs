using System;

namespace MicroPermissions
{
    public class NotHandledPermissionFilterException : Exception
    {
        private const string MessageFormat =
@"Resource '{0}' was not filtered during permission check process.
Please review your PermissionFilter's.
If no change was intended behaviour use {1} = {1} to indicate filter was handled.";

        public NotHandledPermissionFilterException(string resourceTypeName)
            : base(string.Format("", resourceTypeName, nameof(PermissionFilterEventArgs<object>.FilteredResource))) { }
    }
}
