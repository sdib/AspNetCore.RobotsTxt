using System;
using Microsoft.Extensions.DependencyInjection;
using Sdib.AspNetCore.RobotsTxt.Abstractions;
using Sdib.AspNetCore.RobotsTxt.Extensions;

namespace Sdib.AspNetCore.RobotsTxt.Json
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRobotsTxt(this IServiceCollection services,
            Action<RobotsTxtOptions> configureOptions)
        {
            services.AddRobotsTxt();
            services.Configure(configureOptions);
            return services.AddTransient<IRobotGroupProvider, OptionsRobotGroupProvider>();
        }
    }
}
