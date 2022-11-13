using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MicroPermissions.DataAccess.DataLayer
{
    public class DictionaryRuleSet<TContext> : IDataAccessRuleSet where TContext : PermissionContext, IDataLayerPermissionContext
    {
        internal DictionaryRuleSet() { }

        internal Dictionary<Type, IDataAccessRuleSet> Dictionary { get; } = new Dictionary<Type, IDataAccessRuleSet>();

        public bool CanCreate(PermissionContext context, Type type, object @object) => ForType(type)?.CanCreate(context, type, @object) ?? false;

        public bool CanDelete(PermissionContext context, Type type, object @object) => ForType(type)?.CanDelete(context, type, @object) ?? false;

        public bool CanRead(PermissionContext context, Type type, object @object) => ForType(type)?.CanRead(context, type, @object) ?? false;

        public bool CanUpdate(PermissionContext context, Type type, object oldObject, object newObject) => ForType(type)?.CanUpdate(context, type, oldObject, newObject) ?? false;

        public Expression<Func<T, bool>> ReadFilter<T>(PermissionContext context) => ForType(typeof(T))?.ReadFilter<T>(context);

        private IDataAccessRuleSet ForType(Type type)
        {
            if (Dictionary.ContainsKey(type))
                return Dictionary[type];
            else
                return null;
        }
    }
}
