using AutoMapper;
using Holdings.Api.Resources;
using Holdings.Api.Resources.Account;
using Holdings.Core.Models;

namespace Holdings.Api.Config
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserRes>();
            CreateMap<User, AuthenticateRes>();
            CreateMap<User, RegisterRes>();
            CreateMap<User, UpdateRes>();

            CreateMap<Portfolio, PortfolioRes>();
            CreateMap<Model, ModelRes>();
            CreateMap<Holding, HoldingRes>();
        }
    }
}