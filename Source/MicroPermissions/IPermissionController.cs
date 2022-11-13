using System.Threading.Tasks;

namespace MicroPermissions
{
    public interface IPermissionController
    {
        Task<T> FilterAsync<T>(T resource);
        Task<bool> IsGrantedAsync<T>(T request) where T : class, IPermissionRequest;
    }
}