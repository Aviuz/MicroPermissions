using MicroPermissions.DataAccess.Handlers;
using MicroPermissions.DataAccess.Requests;

namespace MicroPermissions.DataAccess.Configuration
{
    public static class RegistryConfiguration
    {
        public static void AddDataLayerModule(this MemoryPermissionHandlerRegistry registry)
        {
            registry.Register<DataLayerPermissionHandler, DataLayerPermissionRequest>();
        }
    }
}
