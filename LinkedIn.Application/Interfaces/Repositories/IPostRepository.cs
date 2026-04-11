using LinkedIn.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIn.Application.Interfaces.Repositories
{
    public interface IPostRepository
    {
        Task<ICollection<PostEntity>> GetAllPostAsync();
        Task<PostEntity> GetPostByIdAsync(int id);
        Task<int> AddPostAsync(PostEntity post);
        Task<int> DeletePostByIdAsync(int id);
        Task<int> UpdatePostByIdAsync(int id , PostEntity post);
    }
}
