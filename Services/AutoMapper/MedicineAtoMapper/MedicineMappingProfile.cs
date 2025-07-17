using AutoMapper;
using Domain.Entities;
using Service.Abstractions.Dtos.CategoryDto;
using Service.Abstractions.Dtos.MedicineDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AutoMapper.MedicineAtoMapper
{
    public class MedicineMappingProfile:Profile
    {
        public MedicineMappingProfile()
        {
            CreateMap<MedicineAddDto, Medicine>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore()); ;
            CreateMap<Medicine, MedicineReadDto>().ReverseMap();
            CreateMap<MedicineUpdateDto, Medicine>().ReverseMap();
        }

    }
}
