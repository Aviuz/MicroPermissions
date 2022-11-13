using PBA.DataLayer;
using System.Threading.Tasks;

namespace PBA.Abstract
{
    public interface IDataAccessRuleSetContainer
    {
        public IDataAccessRuleSet GetRuleSets();
    }

    public interface IDataAccessRuleSetContainerAsync
    {
        public Task<IDataAccessRuleSet> GetRuleSetsAsync();
    }
}
