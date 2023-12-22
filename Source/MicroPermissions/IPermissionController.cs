using System.Threading.Tasks;

namespace MicroPermissions
{
    /// <summary>
    /// Implement this interface if you want custom permission controller. By default this interface is implemented by default MicroPermission.PermissionController.
    /// This controller is responsible for controlling access to resources.
    /// </summary>
    public interface IPermissionController
    {
        Task<T> FilterAsync<T>(T resource);
        Task<bool> IsGrantedAsync<T>(T request);
    }
}