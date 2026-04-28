using AutoMapper;
using LinkedIn.Application.DTOs.ProfileDto;
using LinkedIn.Application.Interfaces.Repositories;
using LinkedIn.Application.Interfaces.Services;
using LinkedIn.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIn.Application.Services
{
    public class ProfileService:IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;
        public ProfileService(IProfileRepository profileRepository,IMapper mapper)
        {
            _profileRepository = profileRepository;
            _mapper = mapper;
        }

        public async Task<ProfileReadDto> AddProfileAsync(Guid userId, ProfileCreateDto profile)
        {
            var profiles = _mapper.Map<ProfileEntity>(profile);
            profiles.UserId = userId;
            var created = await _profileRepository.AddProfileAsync(profiles);
            return _mapper.Map<ProfileReadDto>(created);
        }

        public async Task<bool> DeleteProfileAsync(Guid userId)
        {
            return await _profileRepository.DeleteProfileAsync(userId);
        }

        public async Task<ProfileReadDto?> GetProfileByUserIdAsync(Guid userId)
        {
            var profile = await _profileRepository.GetProfileByUserIdAsync(userId);
            if(profile == null)
            {
                return null;
            }
            return _mapper.Map<ProfileReadDto?>(profile);
        }

        public async Task<string> SaveFileAsync(IFormFile file)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return $"https://localhost:7271/uploads/{fileName}";


        }

        public async Task<ProfileReadDto> UpdateAvatarAsync(Guid userId, IFormFile file)
        {
            var fileUrl = await SaveFileAsync(file);
            var updated = await _profileRepository.UpdateAvatarAsync(userId, fileUrl);
            if (updated == null)
            {
                return null;
            }
            return _mapper.Map<ProfileReadDto>(updated);
            
        }

        public async Task<ProfileReadDto> UpdateBannerAsync(Guid userId, IFormFile file)
        {
            var fileUrl = await SaveFileAsync(file);
            var updated = await _profileRepository.UpdateBannerAsync(userId, fileUrl);
            if(updated == null)
            {
                return null;
            }
            return _mapper.Map<ProfileReadDto>(updated);
        }

        public async Task<ProfileReadDto?> UpdateProfileAsync(Guid userId, ProfileUpdateDto profile)
        {
            var updatedProfile = _mapper.Map<ProfileEntity>(profile);
            var updated = await _profileRepository.UpdateProfileByIdAsync(userId, updatedProfile);
            if(updated == null)
            {
                return null;
            }
            return _mapper.Map<ProfileReadDto>(updated);
        }
        

    }
}
