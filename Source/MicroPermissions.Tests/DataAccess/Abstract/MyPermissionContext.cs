using MicroPermissions.DataAccess;
using MicroPermissions.DataAccess.DataLayer;
using System.Threading.Tasks;

namespace MicroPermissions.Tests.DataAccess.Abstract
{
    public class DataAccessPermissionContext : IDataLayerPermissionContext
    {
        public IDataAccessRuleSet DataAccessRuleSet { get; set; }

        public string UserName { get; set; }

        public Task<IDataAccessRuleSet> GetRuleSetsAsync() => Task.FromResult(DataAccessRuleSet);
    }
}
