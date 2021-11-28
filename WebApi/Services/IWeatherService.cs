using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IWeatherService
    {
        Task<IEnumerable<WeatherEntity>> Get(WeatherQueryRequestModel request);

        Task<bool> Create(WeatherCreateRequestModel model);
    }
}
