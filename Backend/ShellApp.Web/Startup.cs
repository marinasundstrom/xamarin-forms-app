using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShellApp.Application.Common.Interfaces;
using ShellApp.Infrastructure;
using ShellApp.Items;
using ShellApp.Web.Services;

namespace ShellApp.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure(Configuration)
                    .AddItems(Configuration)
                    .AddScoped<ICurrentUserService, CurrentUserService>()
                    .AddScoped<IImageUploader, ImageUploader>();

            services.AddControllers()
                //.AddItems()
                .AddNewtonsoftJson();

            services.AddHttpContextAccessor();

            BlobClientOptions options = new BlobClientOptions(BlobClientOptions.ServiceVersion.V2019_07_07);

            services.AddScoped(sp => new BlobContainerClient(
                Configuration.GetConnectionString("Azure:Storage"), "images", options));

            services.AddScoped<IImageUploader, ImageUploader>();

            services.AddOpenApiDocument(cfg => { cfg.SchemaNameGenerator = new CustomSchemaNameGenerator(); });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
