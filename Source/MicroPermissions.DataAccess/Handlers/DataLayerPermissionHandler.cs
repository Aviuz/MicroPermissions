using MicroPermissions.DataAccess.DataLayer;
using System.Threading.Tasks;

namespace MicroPermissions.DataAccess.Handlers
{
    public class DataLayerPermissionHandler : IPermissionHandler<DataLayerPermissionRequest>
    {
        public async Task HandleRequestAsync(PermissionContext context, DataLayerPermissionRequest request)
        {
            var dataLayerContext = context as IDataLayerPermissionContext;

            IDataAccessRuleSet ruleSet = await dataLayerContext.GetRuleSetsAsync();

            if (request.DataLayerPermission.IsGranted(context, ruleSet))
                context.GrantAccess();
        }
    }
}
