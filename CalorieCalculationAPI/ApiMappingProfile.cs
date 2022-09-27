using AutoMapper;
using CalorieCalculation.API.Contracts;

namespace CalorieCalculation.API
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<CategoryCreateRequest, Core.Category>();
        }
    }
}
