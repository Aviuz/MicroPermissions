using MicroPermissions.DataAccess.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MicroPermissions.DataAccess.DataLayer
{
    public class TypeRuleSet<TContext, T> : IDataAccessRuleSet where TContext : PermissionContext, IDataLayerPermissionContext
    {
        internal List<Func<TContext, T, bool>> Create { get; } = new List<Func<TContext, T, bool>>();
        internal List<Func<TContext, Expression<Func<T, bool>>>> Read { get; } = new List<Func<TContext, Expression<Func<T, bool>>>>();
        internal List<Func<TContext, T, T, bool>> Update { get; } = new List<Func<TContext, T, T, bool>>();
        internal List<Func<TContext, T, bool>> Delete { get; } = new List<Func<TContext, T, bool>>();

        public bool CanCreate(PermissionContext context, Type type, object @object) => Create.Any(c => c.Invoke((TContext)context, (T)@object));

        public bool CanDelete(PermissionContext context, Type type, object @object) => Delete.Any(c => c.Invoke((TContext)context, (T)@object));

        public bool CanRead(PermissionContext context, Type type, object @object) => Read.Any(c => c.Invoke((TContext)context).Compile().Invoke((T)@object));

        public bool CanUpdate(PermissionContext context, Type type, object oldObject, object newObject) => Update.Any(c => c.Invoke((TContext)context, (T)oldObject, (T)newObject));

        public Expression<Func<Y, bool>> ReadFilter<Y>(PermissionContext context)
        {
            if (Read.Count == 0)
                return null;

            var expression = Read[0].Invoke((TContext)context);
            for (int i = 1; i < Read.Count; i++)
                expression = ExpressionUtility.OR(expression, Read[i].Invoke((TContext)context));

            return expression as Expression<Func<Y, bool>>;
        }
    }
}
