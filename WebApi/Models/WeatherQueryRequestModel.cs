using System.Collections.Generic;
using MediatR;

namespace WebApi.Models
{
    public class WeatherQueryRequestModel : IRequest<IEnumerable<WeatherQueryResponseModel>>
    {
        public string City { get; set; }
    }
}
