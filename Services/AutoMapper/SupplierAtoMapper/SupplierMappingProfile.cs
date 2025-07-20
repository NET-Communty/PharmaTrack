using AutoMapper;
using Domain.Entities;
using Service.Abstractions.Dtos.MedicineDto;
using Service.Abstractions.Dtos.SupplierDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AutoMapper.SupplierAtoMapper
{
    public class SupplierMappingProfile:Profile
    {
        public SupplierMappingProfile()
        {
            CreateMap<Supplier, SupplierReadDto>().ReverseMap();
            CreateMap<SupplierAddDto, Supplier>().ReverseMap();
            CreateMap<SupplierUpdateDto, Supplier>().ReverseMap();
        }
    }
}
