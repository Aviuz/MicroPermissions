using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PBA.DataLayer
{
    public class DictionaryRuleSet : IDataAccessRuleSet
    {
        internal DictionaryRuleSet() { }

        internal Dictionary<Type, IDataAccessRuleSet> Dictionary { get; } = new Dictionary<Type, IDataAccessRuleSet>();

        public bool CanCreate(RequestContext context, Type type, object @object) => ForType(type)?.CanCreate(context, type, @object) ?? false;

        public bool CanDelete(RequestContext context, Type type, object @object) => ForType(type)?.CanDelete(context, type, @object) ?? false;

        public bool CanRead(RequestContext context, Type type, object @object) => ForType(type)?.CanRead(context, type, @object) ?? false;

        public bool CanUpdate(RequestContext context, Type type, object oldObject, object newObject) => ForType(type)?.CanUpdate(context, type, oldObject, newObject) ?? false;

        public Expression<Func<T, bool>> ReadFilter<T>(RequestContext context) => ForType(typeof(T))?.ReadFilter<T>(context);

        private IDataAccessRuleSet ForType(Type type)
        {
            if (Dictionary.ContainsKey(type))
                return Dictionary[type];
            else
                return null;
        }
    }
}
