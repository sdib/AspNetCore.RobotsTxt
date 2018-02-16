using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sdib.AspNetCore.RobotsTxt.Abstractions
{
    public class RobotGroup
    {        
        public RobotGroup(string userAgent)
        : this(userAgent, Enumerable.Empty<string>(), Enumerable.Empty<string>())
        {
        }

        public RobotGroup(string userAgent, IEnumerable<string> allow, IEnumerable<string> disallow)
        {
            this.UserAgent = userAgent;
            this.Allow = allow.ToArray();
            this.Disallow = disallow.ToArray();
        }

        public string UserAgent { get; }
        public string[] Allow { get; }
        public string[] Disallow { get; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"User-agent: {this.UserAgent}");

            foreach (var item in Allow)
            {
                builder.Append(Environment.NewLine);
                builder.Append($"Allow: {item}");
            }

            foreach (var item in Disallow)
            {
                builder.Append(Environment.NewLine);
                builder.Append($"Disallow: {item}");
            }
            
            return builder.ToString();
        }
    }
}
