using PBA.Abstract;
using PBA.Handlers;
using PBA.Requests;

namespace Microsoft.Extensions.DependencyInjection
{
    class DependencyInjection
    {
        public IServiceCollection AddPBADataAccess(IServiceCollection services)
        {
            services.AddTransient<PermissionHandler<CreateDatabaseObjectRequest>, CreateObjectHandler>();
            services.AddTransient<PermissionHandler<ReadDatabaseObjectRequest>, ReadObjectHandler>();
            services.AddTransient<PermissionHandler<UpdateDatabaseObjectRequest>, UpdateObjectHandler>();
            services.AddTransient<PermissionHandler<DeleteDatabaseObjectRequest>, DeleteObjectHandler>();
        }
    }
}