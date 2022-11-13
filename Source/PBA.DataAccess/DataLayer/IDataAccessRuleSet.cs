using System;
using System.Linq.Expressions;

namespace PBA.DataLayer
{
    public interface IDataAccessRuleSet
    {
        public bool CanCreate(RequestContext context, Type type, object @object);
        public bool CanRead(RequestContext context, Type type, object @object);
        public bool CanUpdate(RequestContext context, Type type, object oldObject, object newObject);
        public bool CanDelete(RequestContext context, Type type, object @object);
        public Expression<Func<T, bool>> ReadFilter<T>(RequestContext context);
    }
}
