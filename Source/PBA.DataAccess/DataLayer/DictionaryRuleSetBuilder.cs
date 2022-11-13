using System;
using System.Linq.Expressions;

namespace PBA.DataLayer
{
    public class DictionaryRuleSetBuilder
    {
        private DictionaryRuleSet ruleSet = new DictionaryRuleSet();

        public DictionaryRuleSet Build()
        {
            return ruleSet;
        }

        public void AllowRead<T>(Expression<Func<T, bool>> rule)
        {
            ForType<T>().Read.Add(_ => rule);
        }

        public void AllowRead<T>(Func<RequestContext, Expression<Func<T, bool>>> rule)
        {
            ForType<T>().Read.Add(rule);
        }

        public void AllowCreate<T>(Func<RequestContext, T, bool> rule)
        {
            ForType<T>().Create.Add(rule);
        }

        public void AllowDelete<T>(Func<RequestContext, T, bool> rule)
        {
            ForType<T>().Delete.Add(rule);
        }

        public void AllowUpdate<T>(Func<RequestContext, T, T, bool> rule)
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

        private TypeRuleSet<T> ForType<T>()
        {
            if (!ruleSet.Dictionary.ContainsKey(typeof(T)))
                ruleSet.Dictionary.Add(typeof(T), new TypeRuleSet<T>());

            return (TypeRuleSet<T>)ruleSet.Dictionary[typeof(T)];
        }
    }
}
