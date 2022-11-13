using System;
using System.Linq.Expressions;

namespace MicroPermissions.DataAccess.DataLayer
{
    public class DictionaryRuleSetBuilder<TContext> where TContext : IDataLayerPermissionContext
    {
        private DictionaryRuleSet<TContext> ruleSet = new DictionaryRuleSet<TContext>();

        public DictionaryRuleSet<TContext> Build()
        {
            return ruleSet;
        }

        public void AllowRead<T>(Expression<Func<T, bool>> rule)
        {
            ForType<T>().Read.Add(_ => rule);
        }

        public void AllowRead<T>(Func<TContext, Expression<Func<T, bool>>> rule)
        {
            ForType<T>().Read.Add(rule);
        }

        public void AllowCreate<T>(Func<TContext, T, bool> rule)
        {
            ForType<T>().Create.Add(rule);
        }

        public void AllowDelete<T>(Func<TContext, T, bool> rule)
        {
            ForType<T>().Delete.Add(rule);
        }

        public void AllowUpdate<T>(Func<TContext, T, T, bool> rule)
        {
            ForType<T>().Update.Add(rule);
        }

        public void AllowFullAccess<T>()
        {
            ForType<T>().Read.Add(_ => _ => true);
            ForType<T>().Create.Add((_, _) => true);
            ForType<T>().Delete.Add((_, _) => true);
            ForType<T>().Update.Add((_, _, _) => true);
        }

        public void AllowFullAccess<T>(Expression<Func<T, bool>> rule)
        {
            ForType<T>().Read.Add(_ => rule);
            ForType<T>().Create.Add((_, x) => rule.Compile()(x));
            ForType<T>().Delete.Add((_, x) => rule.Compile()(x));
            ForType<T>().Update.Add((_, x, y) => rule.Compile()(x) && rule.Compile()(y));
        }

        private TypeRuleSet<TContext, T> ForType<T>()
        {
            if (!ruleSet.Dictionary.ContainsKey(typeof(T)))
                ruleSet.Dictionary.Add(typeof(T), new TypeRuleSet<TContext, T>());

            return (TypeRuleSet<TContext, T>)ruleSet.Dictionary[typeof(T)];
        }
    }
}
