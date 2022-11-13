using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroPermissions.DataAccess
{
    public abstract class DataLayerPermissionController<TContext> : PermissionController<TContext> where TContext : PermissionContext, IDataLayerPermissionContext
    {
        public DataLayerPermissionController(IPermissionHandlersRegistry registry) : base(registry) { }

        public async Task<IQueryable<T>> WhereHasAccessAsync<T>(TContext context, IQueryable<T> query)
        {
            var ruleSet = await context.GetRuleSetsAsync();

            if (ruleSet != null)
            {
                var predicate = ruleSet.ReadFilter<T>(context);

                if (predicate != null)
                    return query.Where(predicate);
            }

            return new List<T>().AsQueryable();
        }
    }
}
