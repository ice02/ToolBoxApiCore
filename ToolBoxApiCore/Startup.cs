using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;


using ExtCore.WebApplication.Extensions;
using Microsoft.AspNetCore.Mvc.Versioning;
using Swashbuckle.AspNetCore.SwaggerGen;
using ToolBoxApiCore.Utils;
using System.Diagnostics;
using System.Reflection;
using System;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

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

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddExtCore(this.extensionsPath, true);

            services.Configure<SwaggerSettings>(Configuration.GetSection(nameof(SwaggerSettings)));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.AddApiVersioning(o =>
            //{
            //    o.DefaultApiVersion = new ApiVersion(1, 0); // specify the default api version
            //    o.AssumeDefaultVersionWhenUnspecified = true; // assume that the caller wants the default version if they don't specify
            //    o.ApiVersionReader = new MediaTypeApiVersionReader(); // read the version number from the accept header
            //});

            services
                .AddApiVersionWithExplorer()
                .AddSwaggerOptions()
                .AddSwaggerGen();

            //.AddMvcCore()
            //.AddVersionedApiExplorer(options =>
            //{
            //    options.GroupNameFormat = "VVV";
            //    options.SubstituteApiVersionInUrl = true;
            //});

            //services.AddSwaggerDocument(document =>
            //    {
            //        document.DocumentName = "v0";
            //        document.ApiGroupNames = new[] { "0.1" };
            //    })
            //    .AddSwaggerDocument(document =>
            //    {
            //        document.DocumentName = "v1";
            //        document.ApiGroupNames = new[] { "1.0" };
            //    });
            //services.AddSwaggerDocument();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <param name="env">IHostingEnvironment</param>
        /// <param name="provider">Inject temporary IApiVersionDescriptionProvider</param>
        public void Configure(IApplicationBuilder app,
                              IHostingEnvironment env,
                              IApiVersionDescriptionProvider provider)
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

            app.UseSwaggerDocuments();

            app.UseHttpsRedirection();

            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true,
                DefaultContentType = "application/yaml",
                //FileProvider = new PhysicalFileProvider(
                //    Path.Combine(env.WebRootPath, "yaml")),
                //RequestPath = new PathString("/yaml")
            });

            app.UseMvc();

            

            //app.UseSwagger();
            //app.UseSwaggerUi3();
            //app.UseReDoc();
        }
    }

    public sealed class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;
        private readonly SwaggerSettings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureSwaggerGenOptions"/> class.
        /// </summary>
        /// <param name="versionDescriptionProvider">IApiVersionDescriptionProvider</param>
        /// <param name="swaggerSettings">App Settings for Swagger</param>
        public ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider versionDescriptionProvider,
                                          IOptions<SwaggerSettings> swaggerSettings)
        {
            Debug.Assert(versionDescriptionProvider != null, $"{nameof(versionDescriptionProvider)} != null");
            Debug.Assert(swaggerSettings != null, $"{nameof(swaggerSettings)} != null");

            this.provider = versionDescriptionProvider;
            this.settings = swaggerSettings.Value ?? new SwaggerSettings();
        }

        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            //options.DocumentFilter<YamlDocumentFilter>();
            //options.OperationFilter<SwaggerDefaultValues>();

            options.DescribeAllEnumsAsStrings();
            options.IgnoreObsoleteActions();
            options.IgnoreObsoleteProperties();

            AddSwaggerDocumentForEachDiscoveredApiVersion(options);
            //SetCommentsPathForSwaggerJsonAndUi(options);
        }

        private void AddSwaggerDocumentForEachDiscoveredApiVersion(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                settings.Info.Version = description.ApiVersion.ToString();

                if (description.IsDeprecated)
                {
                    settings.Info.Description += " - DEPRECATED";
                }

                options.SwaggerDoc(description.GroupName, settings.Info);
            }
        }

        private static void SetCommentsPathForSwaggerJsonAndUi(SwaggerGenOptions options)
        {
            //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //options.IncludeXmlComments(xmlPath);
        }
    }
}
