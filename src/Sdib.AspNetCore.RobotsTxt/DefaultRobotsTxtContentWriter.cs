using Sdib.AspNetCore.RobotsTxt.Abstractions;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Sdib.AspNetCore.RobotsTxt
{
    public class DefaultRobotsTxtContentWriter : IRobotTxtContentWriter
    {
        private readonly IRobotGroupProvider robotGroupProvider;

        public DefaultRobotsTxtContentWriter(IRobotGroupProvider robotGroupProvider)
        {
            this.robotGroupProvider = robotGroupProvider;
        }

        public async Task<string> WriteAsync()
        {
            StringBuilder builder = new StringBuilder();
            
            builder.Append($"# Generated at {DateTime.UtcNow.ToLocalTime()}");
            
            var groups = await this.robotGroupProvider.GetGroups();
            if (groups != null)
            {
                foreach (var group in groups)
                {
                    builder.AppendLine();
                    builder.Append(group.ToString());
                    builder.AppendLine();
                }
            }

            return builder.ToString();
        }
    }
}
