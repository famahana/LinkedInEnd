using LinkedIn.Application.DTOs.PostDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIn.Application.Interfaces.Services
{
    public interface IPostService
    {
        Task<ICollection<ReadPostDto>> GetAllPostsAsync();
        Task<ReadPostDto> CreatePostAsync(Guid userId, CreatePostDto dto);
    }
}
