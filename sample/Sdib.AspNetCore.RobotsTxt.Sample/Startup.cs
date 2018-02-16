using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Sdib.AspNetCore.RobotsTxt.Abstractions;
using Sdib.AspNetCore.RobotsTxt.Extensions;
using Sdib.AspNetCore.RobotsTxt.Json;

namespace Sdib.AspNetCore.RobotsTxt.Sample
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRobotsTxt(options =>
            {
                options.Groups = new[]
                {
                    new RobotGroup("Google", new[] {"/"}, new[] {"/private"}),
                    new RobotGroup("TwitterBot", new[] {"/", "/hello"}, new[] {"/test"})
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRobotsTxt();            

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
