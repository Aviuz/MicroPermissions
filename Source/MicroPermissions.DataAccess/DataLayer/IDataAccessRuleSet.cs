using System;
using System.Linq.Expressions;

namespace MicroPermissions.DataAccess.DataLayer
{
    public interface IDataAccessRuleSet
    {
        bool CanCreate(IDataLayerPermissionContext context, Type type, object @object);
        bool CanRead(IDataLayerPermissionContext context, Type type, object @object);
        bool CanUpdate(IDataLayerPermissionContext context, Type type, object oldObject, object newObject);
        bool CanDelete(IDataLayerPermissionContext context, Type type, object @object);
        Expression<Func<T, bool>> ReadFilter<T>(IDataLayerPermissionContext context);
    }
}
