using System;
using System.Collections.Generic;

namespace MicroPermissions
{
    public class MemoryPermissionHandlerRegistry : IPermissionHandlersRegistry
    {
        private Dictionary<Type, List<Func<object>>> handlersDictionary = new Dictionary<Type, List<Func<object>>>();

        public MemoryPermissionHandlerRegistry() { }

        public void Register<T, Y>() where T : IPermissionHandler<Y> where Y : IPermissionRequest
        {
            var parameterlessConstructor = typeof(T).GetConstructor(new Type[0]);

            if (parameterlessConstructor == null)
                throw new ArgumentException($"{typeof(T).FullName} does not have parameterless constructor");

            CreateListFor<Y>();

            handlersDictionary[typeof(Y)].Add(() => parameterlessConstructor.Invoke(new object[0]));
        }

        public void Register<T, Y>(Func<T> resolveFunction) where T : class, IPermissionHandler<Y> where Y : IPermissionRequest
        {
            CreateListFor<Y>();

            handlersDictionary[typeof(Y)].Add(resolveFunction);
        }

        public IEnumerable<IPermissionHandler<T>> Resolve<T>() where T : IPermissionRequest
        {
            if (!handlersDictionary.ContainsKey(typeof(T)))
                yield break;

            foreach (var callback in handlersDictionary[typeof(T)])
            {
                var handler = callback.Invoke() as IPermissionHandler<T>;
                yield return handler;
            }
        }

        private void CreateListFor<T>() where T : IPermissionRequest
        {
            if (!handlersDictionary.ContainsKey(typeof(T)))
            {
                handlersDictionary.Add(typeof(T), new List<Func<object>>());
            }
        }
    }
}
