using MicroPermissions.DataAccess.DataLayer;
using System;

namespace MicroPermissions.DataAccess.Requests
{
    public class UpdateDatabaseObjectRequest : IDataLayerPermission
    {
        public Type Type { get; set; }
        public object OldValue { get; set; }
        public object NewValue { get; set; }

        public bool IsGranted(IDataLayerPermissionContext context, IDataAccessRuleSet ruleSet)
           => ruleSet.CanUpdate(context, Type, OldValue, NewValue);
    }
}
