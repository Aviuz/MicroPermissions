using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroPermissions.Tests.Foundation.Abstract
{
    class EmptyNegativeHandler : IPermissionHandler<PermissionContext, EmptyRequest>
    {
        public Task HandleRequestAsync(PermissionContext context, PermissionRequestEventArguments args, EmptyRequest request)
        {
            args.DenyAccess();
            return Task.CompletedTask;
        }
    }
}
