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

        public async Task<int> AddProfileAsync(ProfileEntity profile)
        {
            _linkedInDbContext.Profiles.Add(profile);
            await _linkedInDbContext.SaveChangesAsync();
            return profile.Id;

        }

        public async Task<int> DeleteProfileByIdAsync(int id)
        {
            var profile = await _linkedInDbContext.Profiles.FirstOrDefaultAsync(p=>p.Id == id);
            if(profile ==null)
            {
                return 0;
            }
            _linkedInDbContext.Profiles.Remove(profile);
            await _linkedInDbContext.SaveChangesAsync();
            return profile.Id;

        }

        public async Task<ICollection<ProfileEntity>> GetAllProfileAsync()
        {
            return await _linkedInDbContext.Profiles.ToListAsync();
      
        }

        public async Task<ProfileEntity> GetProfileByIdAsync(int id)
        {
            return await _linkedInDbContext.Profiles.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<int> UpdateProfileByIdAsync(int id, ProfileEntity profile)
        {
            var profiles = await _linkedInDbContext.Profiles.FirstOrDefaultAsync(p => p.Id == id);
            if(profiles == null)
            {
                return 0;
            }
            profiles.Location = profile.Location;
            profiles.Bio = profile.Bio;
            profiles.AvatarUrl = profile.AvatarUrl;
            profiles.FirstName = profile.FirstName;
            profiles.LastName = profile.LastName;
            await _linkedInDbContext.SaveChangesAsync();
            return profile.Id;

        }
    }
}
