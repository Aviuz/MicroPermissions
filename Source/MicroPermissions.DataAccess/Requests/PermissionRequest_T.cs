using MicroPermissions.DataAccess.Handlers;

namespace MicroPermissions.DataAccess.Requests
{
    // permission request as visitor
    public static class PermissionRequests
    {
        public static DataLayerPermissionRequest Create<T>(T @object)
        {
            return new DataLayerPermissionRequest()
            {
                DataLayerPermission = new CreateDatabaseObjectRequest() { Type = typeof(T), Object = @object },
            };
        }

        public static DataLayerPermissionRequest Read<T>(T @object)
        {
            return new DataLayerPermissionRequest()
            {
                DataLayerPermission = new ReadDatabaseObjectRequest() { Type = typeof(T), Object = @object },
            };
        }

        public static DataLayerPermissionRequest Update<T>(T oldObject, T newObject) where T : class
        {
            return new DataLayerPermissionRequest()
            {
                DataLayerPermission = new UpdateDatabaseObjectRequest() { Type = typeof(T), OldValue = oldObject, NewValue = newObject },
            };
        }

        public static DataLayerPermissionRequest Delete<T>(T @object)
        {
            return new DataLayerPermissionRequest()
            {
                DataLayerPermission = new DeleteDatabaseObjectRequest() { Type = typeof(T), Object = @object },
            };
        }
    }
}
