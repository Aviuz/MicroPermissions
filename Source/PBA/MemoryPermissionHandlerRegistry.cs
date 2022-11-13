using PBA.Abstract;
using PBA.Handlers;
using PBA.Requests;
using System;
using System.Collections.Generic;

namespace PBA
{
    public class MemoryPermissionHandlerRegistry : IPermissionHandlersRegistry
    {
        private Dictionary<Type, List<Func<object>>> handlersDictionary = new Dictionary<Type, List<Func<object>>>();

        public MemoryPermissionHandlerRegistry()
        {
           
            Register<ServiceRequestHandler, PermissionRequest>();
        }

        public void Register<T, Y>() where T : PermissionHandler<Y> where Y : IPermissionRequest
        {
            var parameterlessConstructor = typeof(T).GetConstructor(new Type[0]);

            if (parameterlessConstructor == null)
                throw new ArgumentException($"{typeof(T).FullName} does not have parameterless constructor");

            CreateListFor<Y>();

            handlersDictionary[typeof(Y)].Add(() => parameterlessConstructor.Invoke(new object[0]));
        }

        public void Register<T, Y>(Func<T> resolveFunction) where T : PermissionHandler<Y> where Y : class, IPermissionRequest
        {
            CreateListFor<Y>();

            handlersDictionary[typeof(Y)].Add(resolveFunction);
        }

        public IEnumerable<PermissionHandler<T>> Resolve<T>() where T : IPermissionRequest
        {
            if (!handlersDictionary.ContainsKey(typeof(T)))
                yield break;

            foreach (var callback in handlersDictionary[typeof(T)])
            {
                var handler = callback.Invoke() as PermissionHandler<T>;
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
