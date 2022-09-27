using AutoMapper;
using CalorieCalculation.API.Contracts;
using CalorieCalculation.Core;

namespace CalorieCalculation.API
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<UserCreateRequest, User>();
        }
    }
}
