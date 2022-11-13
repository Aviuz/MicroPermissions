using PBA.Abstract;

namespace PBA.Requests
{
    public class PermissionRequest<T> : IPermissionRequest
    {
        public string Permission { get; set; }
        public T Parameter { get; set; }

        public PermissionRequest(string permission, T parameter)
        {
            Permission = permission;
            Parameter = parameter;
        }
    }
}
