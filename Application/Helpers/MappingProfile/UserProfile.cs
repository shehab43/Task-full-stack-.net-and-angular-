using Application.Dtos;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers.MappingProfile
{
   public class EmpolyeeProfile : Profile
    {
        public EmpolyeeProfile() {
            CreateMap<Empolyee,GetAllEmployeesDto>().ReverseMap();
            CreateMap<Empolyee, GetByIdEmployeeDto>().ReverseMap();
            CreateMap<Empolyee, AddEmployeeDto>().ReverseMap();
            CreateMap<Empolyee, UpdateEmployeeDto>().ReverseMap();   
        }
    }
}
