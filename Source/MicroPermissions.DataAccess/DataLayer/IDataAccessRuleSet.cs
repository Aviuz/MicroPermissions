using System;
using System.Linq.Expressions;

namespace MicroPermissions.DataAccess.DataLayer
{
    public interface IDataAccessRuleSet
    {
        bool CanCreate(PermissionContext context, Type type, object @object);
        bool CanRead(PermissionContext context, Type type, object @object);
        bool CanUpdate(PermissionContext context, Type type, object oldObject, object newObject);
        bool CanDelete(PermissionContext context, Type type, object @object);
        Expression<Func<T, bool>> ReadFilter<T>(PermissionContext context);
    }
}
