using System;
using System.Collections.Generic;
using System.Text;

namespace PBA.PermissionBased
{
    public class RegistryConfiguration
    {
        public void Register(IPermissionHandlersRegistry registry)
        {
            registry.Register<CreateObjectHandler, CreateDatabaseObjectRequest>();
            registry.Register<ReadObjectHandler, ReadDatabaseObjectRequest>();
            registry.Register<UpdateObjectHandler, UpdateDatabaseObjectRequest>();
            registry.Register<DeleteObjectHandler, DeleteDatabaseObjectRequest>();
        }
    }
}
