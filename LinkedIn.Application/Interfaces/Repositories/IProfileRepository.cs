using LinkedIn.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIn.Application.Interfaces.Repositories
{
    public interface IProfileRepository
    {
        Task<ProfileEntity?> GetProfileByUserIdAsync(Guid userId);
        Task<ProfileEntity> AddProfileAsync(ProfileEntity profile);
        Task<ProfileEntity?> UpdateProfileByIdAsync(Guid userId,ProfileEntity profile);
        Task<bool> DeleteProfileAsync(Guid userId);
    }
}
