using PBA.Abstract;
using PBA.Requests;
using System.Threading.Tasks;

namespace PBA.Handlers
{
    public class UpdateObjectHandler : PermissionHandler<UpdateDatabaseObjectRequest>
    {
        public override async Task HandleRequestAsync(RequestContext context, UpdateDatabaseObjectRequest request)
        {
            if (context.Identity is IDataAccessRuleSetContainerAsync containerAsync)
            {
                var ruleSet = await containerAsync.GetRuleSetsAsync();

                if (ruleSet.CanUpdate(context, request.Type, request.OldValue, request.NewValue))
                {
                    context.GrantAccess();
                }
            }
            else if (context.Identity is IDataAccessRuleSetContainer container)
            {
                var ruleSet = container.GetRuleSets();

                if (ruleSet.CanUpdate(context, request.Type, request.OldValue, request.NewValue))
                {
                    context.GrantAccess();
                }
            }
        }
    }
}
