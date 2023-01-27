using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StorekeeperAssistant.API.Infrastructure.AutofacModules;
using StorekeeperAssistant.Infrastructure;
using System.Text.Json.Serialization;

namespace StorekeeperAssistant.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public ILifetimeScope AutofacContainer { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<StorekeeperAssistantContext>(options =>
            {

                options
                    .UseNpgsql(Configuration.GetConnectionString("StorekeeperAssistantContext"));
            });

            services.AddControllers().AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            services.AddOptions();
            services.AddSwaggerGen();
        }


        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new MediatorModule());
            builder.RegisterModule(new ApplicationModule(Configuration.GetConnectionString("StorekeeperAssistantContext")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            var pathBase = Configuration["PATH_BASE"];

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseSwagger().UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{(!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty)}/swagger/v1/swagger.json", "StorekeeperAssistant.API V1");
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
