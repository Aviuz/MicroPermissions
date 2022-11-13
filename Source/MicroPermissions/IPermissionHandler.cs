using System.Threading.Tasks;

namespace MicroPermissions
{
    public interface IPermissionHandler<TRequest> where TRequest : IPermissionRequest
    {
        Task HandleRequestAsync(PermissionContext context, TRequest request);
    }
}
