using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MicroPermissions.DataAccess.DataLayer
{
    public class DictionaryRuleSet<TContext> : IDataAccessRuleSet where TContext : IDataLayerPermissionContext
    {
        internal DictionaryRuleSet() { }

        internal Dictionary<Type, IDataAccessRuleSet> Dictionary { get; } = new Dictionary<Type, IDataAccessRuleSet>();

        public bool CanCreate(IDataLayerPermissionContext context, Type type, object @object) => ForType(type)?.CanCreate(context, type, @object) ?? false;

        public bool CanDelete(IDataLayerPermissionContext context, Type type, object @object) => ForType(type)?.CanDelete(context, type, @object) ?? false;

        public bool CanRead(IDataLayerPermissionContext context, Type type, object @object) => ForType(type)?.CanRead(context, type, @object) ?? false;

        public bool CanUpdate(IDataLayerPermissionContext context, Type type, object oldObject, object newObject) => ForType(type)?.CanUpdate(context, type, oldObject, newObject) ?? false;

        public Expression<Func<T, bool>> ReadFilter<T>(IDataLayerPermissionContext context) => ForType(typeof(T))?.ReadFilter<T>(context);

        private IDataAccessRuleSet ForType(Type type)
        {
            if (Dictionary.ContainsKey(type))
                return Dictionary[type];
            else
                return null;
        }
    }
}
