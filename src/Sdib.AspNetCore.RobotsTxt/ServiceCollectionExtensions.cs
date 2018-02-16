using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sdib.AspNetCore.RobotsTxt.Abstractions;

namespace Sdib.AspNetCore.RobotsTxt.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRobotsTxt(this IServiceCollection services)
        {
            services.AddOptions();
            services.TryAddSingleton<IRobotTxtContentWriter, DefaultRobotsTxtContentWriter>();
            return services;
        }
    }
}
