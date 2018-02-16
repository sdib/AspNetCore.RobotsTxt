using Microsoft.AspNetCore.Builder;

namespace Sdib.AspNetCore.RobotsTxt.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseRobotsTxt(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RobotsTxtMiddleware>();
        }
    }
}
