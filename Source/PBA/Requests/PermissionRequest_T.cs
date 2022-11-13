namespace PBA.Requests
{
    // permission request as visitor
    public static class PermissionRequests
    {
        public static CreateDatabaseObjectRequest Create<T>(T @object) => new CreateDatabaseObjectRequest() { Type = typeof(T), Object = @object };
        public static ReadDatabaseObjectRequest Read<T>(T @object) => new ReadDatabaseObjectRequest() { Type = typeof(T), Object = @object };
        public static UpdateDatabaseObjectRequest Update<T>(T oldObject, T newObject) where T : class => new UpdateDatabaseObjectRequest() { Type = typeof(T), OldValue = oldObject, NewValue = newObject };
        public static DeleteDatabaseObjectRequest Delete<T>(T @object) => new DeleteDatabaseObjectRequest() { Type = typeof(T), Object = @object };

        public static PermissionRequest Service(string permission) => new PermissionRequest(permission);
        public static PermissionRequest<T> Service<T>(string permission, T parameter) => new PermissionRequest<T>(permission, parameter);
    }
}
