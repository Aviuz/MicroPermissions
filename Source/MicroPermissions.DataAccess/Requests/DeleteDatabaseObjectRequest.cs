﻿using MicroPermissions.DataAccess.DataLayer;
using System;

namespace MicroPermissions.DataAccess.Requests
{
    public class DeleteDatabaseObjectRequest : IDataLayerPermission
    {
        public Type Type { get; set; }
        public object Object { get; set; }

        public bool IsGranted(IDataLayerPermissionContext context, IDataAccessRuleSet ruleSet)
            => ruleSet.CanDelete(context, Type, Object);
    }
}
