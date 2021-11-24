﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly Settings _settings;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, Settings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        [HttpGet(template: "list")]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)] + $" From {_settings.CurrentIP}:{_settings.Port}"
            })
            .ToArray();
        }

        [HttpGet(template:"list/{id:int}")]
        public string Get(int id)
        {
            return Summaries[id % Summaries.Length];
        }
    }
}
