using MicroPermissions.DataAccess.DataLayer;
using System.Linq;

namespace MicroPermissions.DataAccess.Filters
{
    internal interface IQueryWrapper<TContext> where TContext : IDataLayerPermissionContext
    {
        IQueryable<T> Query<T>(); 
        IQueryWrapper<TContext> WhereCanRead(TContext context, IDataAccessRuleSet ruleSet);
    }
}
