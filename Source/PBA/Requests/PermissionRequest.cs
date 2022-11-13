using PBA.Abstract;

namespace PBA.Requests
{
    public class PermissionRequest : IPermissionRequest
    {
        public string Permission { get; set; }

        public PermissionRequest(string permission)
        {
            Permission = permission;
        }
    }
}
