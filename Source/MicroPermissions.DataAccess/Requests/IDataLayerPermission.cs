using MicroPermissions.DataAccess.DataLayer;

namespace MicroPermissions.DataAccess.Requests
{
    public  interface IDataLayerPermission
    {
        bool IsGranted(IDataLayerPermissionContext context, IDataAccessRuleSet ruleSet);
    }
}
