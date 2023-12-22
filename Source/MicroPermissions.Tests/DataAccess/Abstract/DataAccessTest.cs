using MicroPermissions.DataAccess.Configuration;
using MicroPermissions.DataAccess.DataLayer;
using MicroPermissions.MemoryRegistry;
using System;

namespace MicroPermissions.Tests.DataAccess.Abstract
{
    public class DataAccessTest
    {
        private readonly MemoryMicroPermissionsRegistry<DataAccessPermissionContext> registry = new MemoryMicroPermissionsRegistry<DataAccessPermissionContext>();
        private readonly DataAccessPermissionContext context = new DataAccessPermissionContext();

        public DataAccessTest()
        {
            context.UserName = "currentUser";
        }

        protected void LoginAs(string userName)
        {
            context.UserName = userName;
            registry.AddDataLayerModule();
        }

        protected PermissionController<DataAccessPermissionContext> BuildController(Action<DictionaryRuleSetBuilder<DataAccessPermissionContext>> buildRuleSet)
        {
            var builder = new DictionaryRuleSetBuilder<DataAccessPermissionContext>();
            buildRuleSet.Invoke(builder);
            context.DataAccessRuleSet = builder.Build();

            return new PermissionController<DataAccessPermissionContext>(registry, context, new());
        }
    }
}
