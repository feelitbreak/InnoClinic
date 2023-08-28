using AutoMapper;
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
