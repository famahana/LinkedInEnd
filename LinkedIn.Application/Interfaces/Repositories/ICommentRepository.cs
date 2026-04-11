using LinkedIn.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIn.Application.Interfaces.Repositories
{
    public interface ICommentRepository
    {
        Task<ICollection<CommentEntity>> GetAllCommentAsync();
        Task<CommentEntity> GetCommentByIdAsync(int id);
        Task<int> AddCommentAsync(CommentEntity comment);
        Task<int> UpdateCommentByIdAsync(int id, CommentEntity comment);
        Task<int> DeleteCommentByIdAsync(int id);
    }
}
