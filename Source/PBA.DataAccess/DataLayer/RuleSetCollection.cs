using PBA.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PBA.DataLayer
{
    public class RuleSetCollection : IDataAccessRuleSet
    {
        private List<IDataAccessRuleSet> collection;

        public RuleSetCollection(IEnumerable<IDataAccessRuleSet> ruleSets)
        {
            collection = ruleSets.ToList();
        }

        public bool CanCreate(RequestContext context, Type type, object @object) => collection.Any(r => r.CanCreate(context, type, @object));

        public bool CanDelete(RequestContext context, Type type, object @object) => collection.Any(r => r.CanDelete(context, type, @object));

        public bool CanRead(RequestContext context, Type type, object @object) => collection.Any(r => r.CanRead(context, type, @object));

        public bool CanUpdate(RequestContext context, Type type, object oldObject, object newObject) => collection.Any(r => r.CanUpdate(context, type, oldObject, newObject));

        public Expression<Func<T, bool>> ReadFilter<T>(RequestContext context)
        {
            var expressions = collection
                .Select(c => c.ReadFilter<T>(context))
                .Where(f => f != null)
                .ToList();

            if (expressions.Count() == 0)
                return null;

            var expression = expressions[0];

            for (int i = 1; i < expressions.Count; i++)
                expression = ExpressionUtility.OR(expression, expressions[i]);

            return expression;
        }
    }
}
