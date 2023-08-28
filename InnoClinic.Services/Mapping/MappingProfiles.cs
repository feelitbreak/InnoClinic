using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InnoClinic.Domain.Entities;
using InnoClinic.Domain.DTOs;

namespace InnoClinic.Services.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserSignUpDto, User>();
            CreateMap<OfficeDto, Office>();
        }
    }
}
