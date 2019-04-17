using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using ExtCore.WebApplication.Extensions;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace ToolBoxApiCore
{
    public class Startup
    {
        private string extensionsPath;

        public Startup(IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            Configuration = configuration;

            this.extensionsPath = hostingEnvironment.ContentRootPath + configuration["Extensions:Path"];
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddExtCore(this.extensionsPath, true);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddApiVersioning(o =>
            {
                o.DefaultApiVersion = new ApiVersion(1, 0); // specify the default api version
                o.AssumeDefaultVersionWhenUnspecified = true; // assume that the caller wants the default version if they don't specify
                o.ApiVersionReader = new MediaTypeApiVersionReader(); // read the version number from the accept header
            });

            services.AddSwaggerDocument();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseExtCore();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            

            // Middleware  
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUi3(
            //        config =>
            //{
            //    config.SwaggerRoutes.Add("/swagger/v1/swagger.json");
            //    config.TransformToExternalPath = (internalUiRoute, request) =>
            //    {
            //        // The header X-External-Path is set in the nginx.conf file
            //        var externalPath = request.Headers.ContainsKey("X-External-Path") ? request.Headers["X-External-Path"].First() : "";
            //        return externalPath + internalUiRoute;
            //    };
            //}
            );
            }
        }
    }
}
