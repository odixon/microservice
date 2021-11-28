using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MediatR;
using WebApi.Services;

namespace WebApi
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
            services.AddSingleton(new Settings { CurrentIP = GetLocalIPAddress(), ConsulAddr = Configuration["ConsulAddr"] });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });

            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddSingleton<IWeatherService, WeatherService>();
            services.AddMediatR(typeof(Startup));
        }

        private string GetLocalIPAddress()
        {
            var addr = NetworkInterface.GetAllNetworkInterfaces()
                //.Where(t => t.OperationalStatus == OperationalStatus.Up)
                .Select(t => t.GetIPProperties())
                .SelectMany(t => t.UnicastAddresses)
                .FirstOrDefault(t => t.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork
                && !IPAddress.IsLoopback(t.Address))?.Address.ToString();
            return addr;
        }

        private int GetLocalPort(IApplicationBuilder app)
        {
            var serverAddress = app.ServerFeatures.Get<IServerAddressesFeature>().Addresses.FirstOrDefault();
            var port = serverAddress.Substring(serverAddress.LastIndexOf(':') + 1);
            return int.Parse(port);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger, IHostApplicationLifetime lifetime, Settings settings)
        {
            settings.Port = GetLocalPort(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            RegistConsul(logger, app, lifetime, settings);
        }

        private void RegistConsul(ILogger<Startup> logger, IApplicationBuilder app, IHostApplicationLifetime lifetime, Settings settings)
        {
            var consulClient = new ConsulClient(c =>
            {
                c.Address = new Uri(settings.ConsulAddr);
                c.Datacenter = "dc1";
            });
            
            var serviceRegistration = new AgentServiceRegistration
            {
                ID = Guid.NewGuid().ToString().Replace("-", string.Empty),
                Name = "WeatherForecast",
                Address = settings.CurrentIP,
                Port = settings.Port,
                Check = new AgentServiceCheck
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    Interval = TimeSpan.FromSeconds(10),
                    HTTP = $"http://{settings.CurrentIP}:{settings.Port}/api/healthcheck",
                    Timeout = TimeSpan.FromSeconds(5)
                }
            };
            consulClient.Agent.ServiceRegister(serviceRegistration).Wait();

            logger.LogInformation($"{serviceRegistration.ID} registed successfully.");

            lifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(serviceRegistration.ID);
                logger.LogInformation($"The service {serviceRegistration.ID} deregisted...");
            });
        }
    }
}
