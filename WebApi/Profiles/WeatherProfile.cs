using System;
using AutoMapper;
using WebApi.Models;

namespace WebApi.Profiles
{
    public class WeatherProfile : Profile
    {
        public WeatherProfile()
        {
            CreateMap<WeatherEntity, WeatherQueryResponseModel>().ReverseMap();
        }
    }
}
