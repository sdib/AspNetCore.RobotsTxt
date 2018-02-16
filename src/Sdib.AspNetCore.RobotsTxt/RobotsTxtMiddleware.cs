using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Sdib.AspNetCore.RobotsTxt.Abstractions;

namespace Sdib.AspNetCore.RobotsTxt
{
    public class RobotsTxtMiddleware
    {
        private readonly RequestDelegate next;

        public RobotsTxtMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IRobotTxtContentWriter writer)
        {
            if (!context.Request.Path.Equals("/robots.txt"))
            {
                await next(context);
                return;
            }

            context.Response.ContentType = "text/plain";
            string content = await writer.WriteAsync();
            await context.Response.WriteAsync(content);
        }
    }
}
