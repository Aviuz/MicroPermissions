using System;
using System.Linq.Expressions;

namespace PBA.Utility
{
    public static class ExpressionUtility
    {
        private static string ParamName = "ItemParameter";

        public static Expression<Func<T, bool>> OR<T>(Expression<Func<T, bool>> leftExpression, Expression<Func<T, bool>> rightExpression)
        {
            var param = Expression.Parameter(typeof(T), ParamName);
            return Expression.Lambda<Func<T, bool>>(
                Expression.Or(
                    leftExpression.ReplaceParameter(param).Body,
                    rightExpression.ReplaceParameter(param).Body
                ),
                new ParameterExpression[] { param }
            );
        }

        public static Expression ReplaceSingleParameter(this Expression expression, ParameterExpression oldParam, ParameterExpression newParam)
        {
            var newExpression = new ReplaceParameterVisitor(oldParam, newParam).Visit(expression);
            return newExpression;
        }

        private static Expression<Func<T, bool>> ReplaceParameter<T>(this Expression<Func<T, bool>> expression, ParameterExpression newParam)
        {
            return (Expression<Func<T, bool>>)ReplaceSingleParameter(expression, expression.Parameters[0], newParam);
        }

        private class ReplaceParameterVisitor : ExpressionVisitor
        {
            public ParameterExpression OldParameter { get; set; }
            public ParameterExpression NewParameter { get; set; }

            public ReplaceParameterVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
            {
                OldParameter = oldParameter;
                NewParameter = newParameter;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                if (node == OldParameter)
                    return NewParameter;
                else
                    return node;
            }
        }
    }
}
