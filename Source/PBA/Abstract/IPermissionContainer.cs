using System.Threading.Tasks;

namespace PBA.Abstract
{
    public interface IPermissionContainer
    {
        public bool IsPermissionPresent(string permissionKey);
    }

    public interface IPermissionContainerAsync
    {
        public Task<bool> IsPermissionPresent(string permissionKey);
    }
}
