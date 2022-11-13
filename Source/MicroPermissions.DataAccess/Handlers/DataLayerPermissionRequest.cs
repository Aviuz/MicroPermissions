using MicroPermissions.DataAccess.Requests;

namespace MicroPermissions.DataAccess.Handlers
{
    public class DataLayerPermissionRequest : IPermissionRequest
    {
        public IDataLayerPermission DataLayerPermission { get; set; }
    }
}
