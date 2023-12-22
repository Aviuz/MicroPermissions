using MicroPermissions.DataAccess.DataLayer;
using System;
using System.Linq;

namespace MicroPermissions.DataAccess.Filters
{
    internal class QueryWrapper<TContext, TResource> : IQueryWrapper<TContext> where TContext : IDataLayerPermissionContext
    {
        private readonly IQueryable<TResource> query;

        public QueryWrapper(IQueryable<TResource> query)
        {
            this.query = query;
        }

        public IQueryable<T> Query<T>()
        {
            if (!typeof(T).IsAssignableFrom(typeof(TResource)))
                throw new ArgumentException($"Failed to cast QueryWrapper of type <{typeof(TResource).Name}> to type <{typeof(T).Name}>");
            return query as IQueryable<T>;
        }

        public IQueryWrapper<TContext> WhereCanRead(TContext context, IDataAccessRuleSet ruleSet)
        {
            var predicate = ruleSet.ReadFilter<TResource>(context);

            if (predicate != null)
            {
                return new QueryWrapper<TContext, TResource>(query.Where(predicate));
            }
            else
            {
                return new QueryWrapper<TContext, TResource>(Enumerable.Empty<TResource>().AsQueryable());
            }
        }
    }
}
