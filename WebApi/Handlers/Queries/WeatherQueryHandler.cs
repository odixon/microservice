using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Handlers.Queries
{
    public class WeatherQueryHandler : IRequestHandler<WeatherQueryRequestModel, IEnumerable<WeatherQueryResponseModel>>
    {
        private readonly IWeatherService _weatherService;
        private readonly IMapper _mapper;
        private readonly ILogger<WeatherQueryHandler> _logger;

        public WeatherQueryHandler(IWeatherService weatherService, IMapper mapper, ILogger<WeatherQueryHandler> logger)
        {
            _weatherService = weatherService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<WeatherQueryResponseModel>> Handle(WeatherQueryRequestModel request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("使用Automapper。。。");
            var result = await _weatherService.Get(request);
            return result.Select(x => _mapper.Map<WeatherQueryResponseModel>(x));
        }
    }
}
