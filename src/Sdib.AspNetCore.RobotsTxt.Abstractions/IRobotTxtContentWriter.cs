using System.Threading.Tasks;

namespace Sdib.AspNetCore.RobotsTxt.Abstractions
{
    public interface IRobotTxtContentWriter
    {
        Task<string> WriteAsync();
    }
}
