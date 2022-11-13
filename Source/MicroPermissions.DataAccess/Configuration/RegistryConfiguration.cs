using MicroPermissions.DataAccess.Filters;
using MicroPermissions.DataAccess.Handlers;
using MicroPermissions.MemoryRegistry;

namespace MicroPermissions.DataAccess.Configuration
{
    public static class RegistryConfiguration
    {
        public static void AddDataLayerModule<TContext>(this MemoryMicroPermissionsRegistry<TContext> registry) where TContext : IDataLayerPermissionContext
        {
            registry.RegisterHandler<DataLayerPermissionHandler<TContext>, DataLayerPermissionRequest>();
            registry.RegisterFilter<QueryFilter<TContext>, IQueryWrapper<TContext>>();
        }
    }
}
