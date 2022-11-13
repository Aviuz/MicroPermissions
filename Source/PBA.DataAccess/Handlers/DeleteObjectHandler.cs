using PBA.Abstract;
using PBA.Requests;
using System.Threading.Tasks;

namespace PBA.Handlers
{
    public class DeleteObjectHandler : PermissionHandler<DeleteDatabaseObjectRequest>
    {
        public override async Task HandleRequestAsync(RequestContext context, DeleteDatabaseObjectRequest request)
        {
            if (context.Identity is IDataAccessRuleSetContainerAsync containerAsync)
            {
                var ruleSet = await containerAsync.GetRuleSetsAsync();

                if (ruleSet.CanDelete(context, request.Type, request.Object))
                {
                    context.GrantAccess();
                }
            }
            else if (context.Identity is IDataAccessRuleSetContainer container)
            {
                var ruleSet = container.GetRuleSets();

                if (ruleSet.CanDelete(context, request.Type, request.Object))
                {
                    context.GrantAccess();
                }
            }
        }
    }
}
