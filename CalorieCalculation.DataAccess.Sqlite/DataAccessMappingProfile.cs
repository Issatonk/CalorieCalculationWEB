using AutoMapper;
using CalorieCalculation.DataAccess.Sqlite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieCalculation.DataAccess.Sqlite
{
    public class DataAccessMappingProfile : Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<Core.Product, Product>().ReverseMap();
            CreateMap<Core.Category, Category>().ReverseMap();
            CreateMap<Core.User, User>().ReverseMap();
            CreateMap<Core.FoodConsumed, FoodConsumed>().ReverseMap();
        }
    }
}
