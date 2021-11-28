using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WebApi.Models;

namespace WebApi.Services
{
    public class WeatherService : IWeatherService
    {
        private IList<string> _cities = new List<string>
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly Settings _settings;
        private readonly ILogger<WeatherService> _logger;

        public WeatherService(Settings settings, ILogger<WeatherService> logger)
        {
            _settings = settings;
            _logger = logger;
        }

        public Task<bool> Create(WeatherCreateRequestModel model)
        {
            _cities.Add(model.City);
            return Task.FromResult(true);
        }

        public Task<IEnumerable<WeatherEntity>> Get(WeatherQueryRequestModel request)
        {
            _logger.LogInformation($"{nameof(WeatherService)} was called.");

            var random = new Random();
            return Task.FromResult(_cities.Select(x => new WeatherEntity
            {
                Date = DateTime.Now.AddDays(random.Next(1, 10)),
                Summary = $"{x}, From {_settings.CurrentIP}:{_settings.Port}",
                TemperatureC = random.Next(-20, 30)
            }).Where(x => x.Summary.Contains(request.City)));
        }
    }
}
