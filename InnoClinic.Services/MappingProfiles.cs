﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InnoClinic.Domain.Entities;

namespace InnoClinic.Services
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<SignUpUserModel, User>();
            CreateMap<SIgnInUserModel, User>();
        }
    }
}
