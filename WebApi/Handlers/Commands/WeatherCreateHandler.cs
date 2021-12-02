using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Handlers.Commands
{
    public class WeatherCreateHandler : IRequestHandler<WeatherCreateRequestModel, WeatherCreateResponseModel>
    {
        private readonly IWeatherService _weatherService;

        public WeatherCreateHandler(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public async Task<WeatherCreateResponseModel> Handle(WeatherCreateRequestModel request, CancellationToken cancellationToken)
        {
            var result = await _weatherService.Create(request);
            return new WeatherCreateResponseModel { Success = result };
        }
    }
}
