using System.Collections.Generic;

namespace MicroPermissions
{
    public interface IMicroPermissionsRegistry<TContext>
    {
        IEnumerable<IPermissionHandler<TContext, T>> ResolveHandler<T>();
        IEnumerable<IPermissionFilter<TContext, T>> ResolveFilter<T>();
    }
}
