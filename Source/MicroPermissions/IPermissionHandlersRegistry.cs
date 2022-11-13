using System.Collections.Generic;

namespace MicroPermissions
{
    public interface IMicroPermissionsRegistry<TContext>
    {
        IEnumerable<IPermissionHandler<TContext, T>> ResolveHandler<T>() where T : IPermissionRequest;
        IEnumerable<IPermissionFilter<TContext, T>> ResolveFilter<T>();
    }
}
