using AutoMapper;
using LinkedIn.Application.DTOs.PostDto;
using LinkedIn.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIn.Application.Mapping
{
    public class PostProfile:Profile
    {
        public PostProfile()
        {
            CreateMap<CreatePostDto, PostEntity>();

            CreateMap<PostEntity, ReadPostDto>()
                .ForMember(dest => dest.FirstName,
                    opt => opt.MapFrom(src => src.User.Profile.FirstName))
                 .ForMember(dest => dest.LastName,
                    opt => opt.MapFrom(src => src.User.Profile.LastName))
                 .ForMember(dest => dest.Company,
                    opt => opt.MapFrom(src => src.User.Profile.Company))
                 .ForMember(dest => dest.Position,
                    opt => opt.MapFrom(src => src.User.Profile.Position))
                .ForMember(dest => dest.AuthorAvatar,
                    opt => opt.MapFrom(src => src.User.Profile.AvatarUrl));
        }

    }
}
