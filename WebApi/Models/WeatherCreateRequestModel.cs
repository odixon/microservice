using MediatR;

namespace WebApi.Models
{
    public class WeatherCreateRequestModel : IRequest<WeatherCreateResponseModel>
    {
        public string City { get; set; }
    }
}
