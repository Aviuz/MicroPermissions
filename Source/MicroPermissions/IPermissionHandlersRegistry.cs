using System.Collections.Generic;

namespace MicroPermissions
{
    public interface IPermissionHandlersRegistry
    {
        IEnumerable<IPermissionHandler<T>> Resolve<T>() where T : IPermissionRequest;
    }
}
