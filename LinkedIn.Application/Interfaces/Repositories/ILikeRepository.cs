using LinkedIn.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIn.Application.Interfaces.Repositories
{
    public interface ILikeRepository
    {
        Task<ICollection<LikeEntity>> GetAllLikeAsync();
        Task<LikeEntity> GetLikeByIdAsync(int id);
        Task<int> AddLikeAsync(LikeEntity entity);
        Task<int> UpdateLikeByIdAsync(int id,LikeEntity entity);
        Task<int> DeleteLikeByIdAsync(int id);

    }
}
