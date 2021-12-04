using System;
using AutoMapper;
using WebApi.Models;

namespace WebApi.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Entities.User, UserModel>()
                .ForMember(dest => dest.Age, opts => opts.MapFrom(source => (DateTime.Now - source.Birthday).TotalDays / 365));

            CreateMap<CreateUserRequestModel, Entities.User>()
                .ForMember(dest => dest.Birthday, opts => opts.MapFrom(source => DateTime.Now.AddYears(-source.Age)));
        }
    }
}
