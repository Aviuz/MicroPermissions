using System.Threading.Tasks;

namespace MicroPermissions.DataAccess.Filters
{
    internal class QueryFilter<TContext> : IPermissionFilter<TContext, IQueryWrapper<TContext>> where TContext : IDataLayerPermissionContext
    {
        public async Task FilterResourceAsync(TContext context, PermissionFilterEventArgs<IQueryWrapper<TContext>> args)
        {
            var ruleSet = await context.GetRuleSetsAsync();

            if (ruleSet != null)
            {
                args.FilteredResource = args.FilteredResource.WhereCanRead(context, ruleSet);
            }
        }
    }
}
