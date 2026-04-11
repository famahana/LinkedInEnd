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
    public class PostRepository:IPostRepository
    {
        private readonly LinkedInDbContext _linkedInDbContext;
        public PostRepository(LinkedInDbContext context)
        {
            _linkedInDbContext = context;
        }

        public async Task<int> AddPostAsync(PostEntity post)
        {
            _linkedInDbContext.Posts.Add(post);
            await _linkedInDbContext.SaveChangesAsync();
            return post.Id;
        }

        public async Task<int> DeletePostByIdAsync(int id)
        {
            var post = await _linkedInDbContext.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if(post == null)
            {
                return 0;
            }
            _linkedInDbContext.Posts.Remove(post);
            await _linkedInDbContext.SaveChangesAsync();
            return post.Id;
        }

        public async Task<ICollection<PostEntity>> GetAllPostAsync()
        {
            return await _linkedInDbContext.Posts.ToListAsync();
        }

        public async Task<PostEntity> GetPostByIdAsync(int id)
        {
            return await _linkedInDbContext.Posts.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<int> UpdatePostByIdAsync(int id, PostEntity post)
        {
            var posts = await _linkedInDbContext.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if(posts == null)
            {
                return 0;
            }
            posts.Content = post.Content;
            await _linkedInDbContext.SaveChangesAsync();
            return posts.Id;
        }
    }
}
