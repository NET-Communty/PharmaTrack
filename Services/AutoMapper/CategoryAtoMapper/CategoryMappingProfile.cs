using AutoMapper;
using Domain.Entities;
using Service.Abstractions.Dtos.CategoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AutoMapper.CategoryAtoMapper
{
    public class CategoryMappingProfile:Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<CategoryAddDto, Category>();
            CreateMap<Category,CategoryReadDto>().ReverseMap();
            CreateMap<CategoryUpdateDto,Category>().ReverseMap();

        }
    }
}
