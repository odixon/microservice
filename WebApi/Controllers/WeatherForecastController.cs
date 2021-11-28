using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WeatherForecastController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<IEnumerable<WeatherQueryResponseModel>> Get([FromQuery]WeatherQueryRequestModel model)
        {
            return _mediator.Send(model);
        }

        [HttpPost]
        public Task<WeatherCreateResponseModel> Post(WeatherCreateRequestModel model)
        {
            return _mediator.Send(model);
        }
    }
}
