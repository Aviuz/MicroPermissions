using PBA.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PBA
{
    public class PermissionController
    {
        private readonly IPermissionHandlersRegistry registry;

        public PermissionController(IPermissionHandlersRegistry registry)
        {
            this.registry = registry;
        }

        public async Task<bool> IsGrantedAsync<T>(object identity, T request) where T : IPermissionRequest
        {
            var context = new RequestContext() { Identity = identity, };

            foreach (var handler in registry.Resolve<T>())
            {
                await handler.HandleRequestAsync(context, request);
            }

            return context.Success;
        }

        public async Task<IQueryable<T>> WhereHasAccessAsync<T>(object identity, IQueryable<T> query)
        {
            var context = new RequestContext() { Identity = identity, };

            if (context.Identity is IDataAccessRuleSetContainerAsync containerAsync)
            {
                var ruleSet = await containerAsync.GetRuleSetsAsync();

                if (ruleSet != null)
                {
                    var predicate = ruleSet.ReadFilter<T>(context);

                    if (predicate != null)
                        return query.Where(predicate);
                }

            }
            else if (context.Identity is IDataAccessRuleSetContainer container)
            {
                var ruleSet = container.GetRuleSets();

                if (ruleSet != null)
                {
                    var predicate = ruleSet.ReadFilter<T>(context);

                    if (predicate != null)
                        return query.Where(predicate);
                }
            }

            return new List<T>().AsQueryable();
        }
    }
}
