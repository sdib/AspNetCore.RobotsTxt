using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Sdib.AspNetCore.RobotsTxt.Abstractions;
using Sdib.AspNetCore.RobotsTxt.Json;

namespace Sdib.AspNetCore.RobotsTxt
{
    public class OptionsRobotGroupProvider : IRobotGroupProvider
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IOptions<RobotsTxtOptions> options;

        public OptionsRobotGroupProvider(IHostingEnvironment hostingEnvironment,
            IOptions<RobotsTxtOptions> options)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.options = options;
        }

        public Task<RobotGroup[]> GetGroups()
        {
            return Task.FromResult(this.options.Value.Groups);
        }
    }
}
