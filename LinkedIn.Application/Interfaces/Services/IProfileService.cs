using LinkedIn.Application.DTOs.ProfileDto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIn.Application.Interfaces.Services
{
    public interface IProfileService
    {
        Task<ProfileReadDto?> GetProfileByUserIdAsync(Guid userId);
        Task<ProfileReadDto> AddProfileAsync(Guid userId,ProfileCreateDto profile);
        Task<ProfileReadDto?> UpdateProfileAsync(Guid userId, ProfileUpdateDto profile);
        Task<bool> DeleteProfileAsync(Guid userId);
        Task<string> SaveFileAsync(IFormFile file);
        Task<ProfileReadDto> UpdateAvatarAsync(Guid userId,IFormFile file);
        Task<ProfileReadDto> UpdateBannerAsync(Guid userId,IFormFile file);



    }
}
