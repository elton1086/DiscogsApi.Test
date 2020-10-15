using DiscogsApi.Contrats.Proxys;
using DiscogsApi.Contrats.Services;
using DiscogsApi.Proxys;
using DiscogsApi.Proxys.Configurations;
using DiscogsApi.Services;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DiscogsApi
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
            services.AddControllers();
            ConfigurationsInjectionDependance(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigurationsInjectionDependance(IServiceCollection services)
        {
            services.Configure<ConfigurationProxy>(Configuration.GetSection(ConfigurationProxy.NomSection));
            services.AddScoped<IDiscogsApiProxy, DiscogsApiProxy>();
            services.AddScoped<ICollectionService, CollectionService>();
        }
    }
}
