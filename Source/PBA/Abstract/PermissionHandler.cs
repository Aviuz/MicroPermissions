using System.Threading.Tasks;

namespace PBA.Abstract
{
    public abstract class PermissionHandler<T> where T : IPermissionRequest
    {
        public abstract Task HandleRequestAsync(RequestContext context, T request);
    }
}
