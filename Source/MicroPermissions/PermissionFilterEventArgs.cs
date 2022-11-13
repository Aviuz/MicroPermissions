namespace MicroPermissions
{
    public class PermissionFilterEventArgs<TResource>
    {
        private bool accessDenied = false;
        private TResource filteredResouce = default;

        /// <summary>
        /// This is original resource passed to permission controller.
        /// </summary>
        public TResource OriginalResource { get; }
        /// <summary>
        /// This is output resource that will be delivered to user. If unchanged it will point to original source and property ResourceChanged will be equal to false.
        /// </summary>
        public TResource FilteredResource { get => filteredResouce; set { ResourceChanged = true; filteredResouce = value; } }

        /// <summary>
        /// If true FilteredResource was changed by one of filters
        /// </summary>
        public bool ResourceChanged { get; private set; }

        /// <summary>
        /// If true one of filters indicated that whole resource is forbidden
        /// </summary>
        public bool AccessDenied => accessDenied;

        public PermissionFilterEventArgs(TResource originalResource)
        {
            OriginalResource = originalResource;
            filteredResouce = originalResource;
        }

        /// <summary>
        /// Indicate that whole resource is forbidden. It will override other filters.
        /// </summary>
        public void DenyAccess()
        {
            accessDenied = true;
        }
    }
}
