using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;

namespace Gateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOcelot()
                .AddConsul();

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication("Ids4Key", option => {
                    option.Authority = Configuration.GetValue<string>("AccessTokenUrl");
                    option.ApiName = "WeatherApi";
                    option.SupportedTokens = IdentityServer4.AccessTokenValidation.SupportedTokens.Both;
                    option.RequireHttpsMetadata = false;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseOcelot();
        }
    }
}
