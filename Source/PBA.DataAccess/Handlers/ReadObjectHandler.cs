using PBA.Abstract;
using PBA.Requests;
using System.Threading.Tasks;

namespace PBA.Handlers
{
    public class ReadObjectHandler : PermissionHandler<ReadDatabaseObjectRequest>
    {
        public override async Task HandleRequestAsync(RequestContext context, ReadDatabaseObjectRequest request)
        {
            if (context.Identity is IDataAccessRuleSetContainerAsync containerAsync)
            {
                var ruleSet = await containerAsync.GetRuleSetsAsync();

                if (ruleSet.CanRead(context, request.Type, request.Object))
                {
                    context.GrantAccess();
                }
            }
            else if (context.Identity is IDataAccessRuleSetContainer container)
            {
                var ruleSet = container.GetRuleSets();

                if (ruleSet.CanRead(context, request.Type, request.Object))
                {
                    context.GrantAccess();
                }
            }
        }
    }
}
