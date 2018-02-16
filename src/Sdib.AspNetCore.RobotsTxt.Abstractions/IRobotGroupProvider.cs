using System.Threading.Tasks;

namespace Sdib.AspNetCore.RobotsTxt.Abstractions
{
    public interface IRobotGroupProvider
    {
        Task<RobotGroup[]> GetGroups();
    }
}
