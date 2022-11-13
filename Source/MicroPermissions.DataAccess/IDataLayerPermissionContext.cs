using MicroPermissions.DataAccess.DataLayer;
using System.Threading.Tasks;

namespace MicroPermissions.DataAccess
{
    public interface IDataLayerPermissionContext
    {
        Task<IDataAccessRuleSet> GetRuleSetsAsync();
    }
}
