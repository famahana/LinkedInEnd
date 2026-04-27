using AutoMapper;
using LinkedIn.Application.DTOs.ProfileDto;
using LinkedIn.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIn.Application.Mapping
{
    public class ProfileProfile:Profile
    {
        public ProfileProfile()
        {
            CreateMap<ProfileCreateDto, ProfileEntity>();

            CreateMap<ProfileUpdateDto, ProfileEntity>();

            CreateMap<ProfileEntity, ProfileReadDto>();
        }
    }
}
