using MicroPermissions.DataAccess.DataLayer;
using System.Threading.Tasks;

namespace MicroPermissions.DataAccess.Handlers
{
    public class DataLayerPermissionHandler<TContext> : IPermissionHandler<TContext, DataLayerPermissionRequest> where TContext : IDataLayerPermissionContext
    {
        public async Task HandleRequestAsync(TContext context, PermissionRequestEventArguments args, DataLayerPermissionRequest request)
        {
            IDataAccessRuleSet ruleSet = await context.GetRuleSetsAsync();

            if (request.DataLayerPermission.IsGranted(context, ruleSet))
                args.GrantAccess();
        }
    }
}
