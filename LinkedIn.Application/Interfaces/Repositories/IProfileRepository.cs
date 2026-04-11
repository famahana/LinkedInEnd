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
        Task<ICollection<ProfileEntity>> GetAllProfileAsync();
        Task<ProfileEntity> GetProfileByIdAsync(int id);
        Task<int> AddProfileAsync(ProfileEntity profile);
        Task<int> DeleteProfileByIdAsync(int id);
        Task<int> UpdateProfileByIdAsync(int id, ProfileEntity profile);
    }
}
