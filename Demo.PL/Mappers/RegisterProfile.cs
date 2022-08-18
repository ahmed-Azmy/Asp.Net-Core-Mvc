using AutoMapper;
using Demo.PL.Models;
using Microsoft.AspNetCore.Identity;

namespace Demo.PL.Mappers
{
    public class RegisterProfile : Profile
    {
        public RegisterProfile()
        {
            CreateMap<IdentityUser, RegisterViewModel>().ReverseMap();
        }
    }
}
