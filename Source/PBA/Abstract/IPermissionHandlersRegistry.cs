using PBA.Abstract;
using System.Collections.Generic;

namespace PBA
{
    public interface IPermissionHandlersRegistry
    {
        public IEnumerable<PermissionHandler<T>> Resolve<T>() where T : IPermissionRequest;
    }
}
