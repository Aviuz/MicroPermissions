using System;
using System.Collections.Generic;

namespace MicroPermissions.MemoryRegistry
{
    internal class TypedRegistry
    {
        private Dictionary<Type, List<Func<object>>> dictionary = new Dictionary<Type, List<Func<object>>>();

        public void Register<TKey, TItem>() where TItem : class
        {
            var parameterlessConstructor = typeof(TItem).GetConstructor(new Type[0]);

            if (parameterlessConstructor == null)
                throw new ArgumentException($"{typeof(TItem).FullName} does not have parameterless constructor");

            CreateListFor<TKey>();

            dictionary[typeof(TKey)].Add(() => parameterlessConstructor.Invoke(new object[0]));
        }

        public void Register<TKey>(Func<object> resolveFunction)
        {
            CreateListFor<TKey>();

            dictionary[typeof(TKey)].Add(resolveFunction);
        }

        public IEnumerable<TOutput> Resolve<TKey, TOutput>() where TOutput : class
        {
            if (!dictionary.ContainsKey(typeof(TKey)))
                yield break;

            foreach (var callback in dictionary[typeof(TKey)])
            {
                var handler = callback.Invoke() as TOutput;
                yield return handler;
            }
        }

        private void CreateListFor<TRequest>()
        {
            if (!dictionary.ContainsKey(typeof(TRequest)))
            {
                dictionary.Add(typeof(TRequest), new List<Func<object>>());
            }
        }
    }
}
