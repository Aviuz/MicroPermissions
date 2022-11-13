using PBA.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PBA.DataLayer
{
    public class TypeRuleSet<T> : IDataAccessRuleSet
    {
        internal List<Func<RequestContext, T, bool>> Create { get; } = new List<Func<RequestContext, T, bool>>();
        internal List<Func<RequestContext, Expression<Func<T, bool>>>> Read { get; } = new List<Func<RequestContext, Expression<Func<T, bool>>>>();
        internal List<Func<RequestContext, T, T, bool>> Update { get; } = new List<Func<RequestContext, T, T, bool>>();
        internal List<Func<RequestContext, T, bool>> Delete { get; } = new List<Func<RequestContext, T, bool>>();

        public bool CanCreate(RequestContext context, Type type, object @object) => Create.Any(c => c.Invoke(context, (T)@object));

        public bool CanDelete(RequestContext context, Type type, object @object) => Delete.Any(c => c.Invoke(context, (T)@object));

        public bool CanRead(RequestContext context, Type type, object @object) => Read.Any(c => c.Invoke(context).Compile().Invoke((T)@object));

        public bool CanUpdate(RequestContext context, Type type, object oldObject, object newObject) => Update.Any(c => c.Invoke(context, (T)oldObject, (T)newObject));

        public Expression<Func<Y, bool>> ReadFilter<Y>(RequestContext context)
        {
            if (Read.Count == 0)
                return null;

            var expression = Read[0].Invoke(context);
            for (int i = 1; i < Read.Count; i++)
                expression = ExpressionUtility.OR(expression, Read[i].Invoke(context));

            return expression as Expression<Func<Y, bool>>;
        }
    }
}
