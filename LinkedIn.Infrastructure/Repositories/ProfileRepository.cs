using LinkedIn.Application.Interfaces.Repositories;
using LinkedIn.Domain.Entities;
using LinkedIn.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIn.Infrastructure.Repositories
{
    public class ProfileRepository:IProfileRepository
    {
        private readonly LinkedInDbContext _linkedInDbContext;
        public ProfileRepository(LinkedInDbContext context)
        {
            _linkedInDbContext = context;
        }

        public async Task<ProfileEntity> AddProfileAsync(ProfileEntity profile)
        {
            _linkedInDbContext.Profiles.Add(profile);
            await _linkedInDbContext.SaveChangesAsync();
            return profile;

        }

        public async Task<bool> DeleteProfileAsync(Guid userId)
        {
            var profile = await _linkedInDbContext.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);
            if(profile == null)
            {
                return false;
            }
            _linkedInDbContext.Profiles.Remove(profile);
            await _linkedInDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<ProfileEntity?> GetProfileByUserIdAsync(Guid userId)
        {
            return await _linkedInDbContext.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<ProfileEntity?> UpdateProfileByIdAsync(Guid userId,ProfileEntity profile)
        {
            var profiles = await _linkedInDbContext.Profiles.FirstOrDefaultAsync(p=>p.UserId == userId);
            if(profiles == null)
            {
                return null;
            }
            profiles.FirstName = profile.FirstName;
            profiles.LastName = profile.LastName;
            profiles.Bio = profile.Bio;
            profiles.Company = profile.Company;
            profiles.Position = profile.Position;
            profiles.Location = profile.Location;
            await _linkedInDbContext.SaveChangesAsync();
            return profiles;  
        }
        
    }
}
