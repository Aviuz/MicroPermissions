using PBA.Abstract;
using PBA.Requests;
using System.Threading.Tasks;

namespace PBA.Handlers
{
    public class ServiceRequestHandler : PermissionHandler<PermissionRequest>
    {
        public async override Task HandleRequestAsync(RequestContext context, PermissionRequest request)
        {
            if (context.Identity is IPermissionContainerAsync containerAsync)
            {
                if (await containerAsync.IsPermissionPresent(request.Permission))
                {
                    context.GrantAccess();
                }
            }
            else if (context.Identity is IPermissionContainer container)
            {
                if (container.IsPermissionPresent(request.Permission))
                {
                    context.GrantAccess();
                }
            }
        }
    }
}
