using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vending_machine.Models;

namespace vending_machine.DTOs.Mappings
{
  
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Drink, DrinkDto>().ReverseMap();
            CreateMap<DrinkDto, Drink>().ReverseMap();
        }

    }
}
