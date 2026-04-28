using AutoMapper;
using LinkedIn.Application.DTOs.PostDto;
using LinkedIn.Application.Interfaces.Repositories;
using LinkedIn.Application.Interfaces.Services;
using LinkedIn.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIn.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        public PostService(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }
        public async Task<ReadPostDto> CreatePostAsync(Guid userId, CreatePostDto dto)
        {
            var post = _mapper.Map<PostEntity>(dto);
            post.UserId = userId;
            post.CreatedAt = DateTime.UtcNow;
            await _postRepository.AddPostAsync(post);
            var result = await _postRepository.GetPostWithDetailsAsync(post.Id);
            return _mapper.Map<ReadPostDto>(result);   
        }

        public async Task<ICollection<ReadPostDto>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllPostAsync();
            return _mapper.Map<ICollection<ReadPostDto>>(posts);
        }
    }
}
