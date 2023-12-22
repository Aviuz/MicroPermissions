namespace MicroPermissions
{
    public class PermissionControllerOptions
    {
        /// <summary>
        /// If set to false it will not throw NotHandledPermissionFilterException or NotHandledPermissionHandlerException when no filter or handler are found. It is by default set to true.
        /// Idea of this setting is to ensure developer do not forget to implement permission handler when permission controller is used.
        /// </summary>
        public bool ThrowIfUnhandled { get; set; } = true;
    }
}
