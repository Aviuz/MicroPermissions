using MicroPermissions.DataAccess.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace MicroPermissions.DataAccess
{
    public static class DataLayerPermissionControllerExtensions
    {
        public static async Task<IQueryable<T>> WhereHasAccessAsync<TContext, T>(this IQueryable<T> query, PermissionController<TContext> permissionController) where TContext : IDataLayerPermissionContext
        {
            var result = await permissionController.FilterAsync<IQueryWrapper<TContext>>(new QueryWrapper<TContext, T>(query));
            return result.Query<T>();
        }

        public static async Task<IQueryable<T>> WhereHasAccessAsync<TContext, T>(this IQueryable<T> query, IPermissionController permissionController) where TContext : IDataLayerPermissionContext
        {
            var result = await permissionController.FilterAsync<IQueryWrapper<TContext>>(new QueryWrapper<TContext, T>(query));
            return result.Query<T>();
        }
    }
}
