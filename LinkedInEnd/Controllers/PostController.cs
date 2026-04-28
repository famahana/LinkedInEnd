using LinkedIn.Application.DTOs.PostDto;
using LinkedIn.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LinkedIn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController(IPostService _postService) : ControllerBase
    {
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostDto dto)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var post = await _postService.CreatePostAsync(userId, dto);
            return Ok(post);
        }
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postService.GetAllPostsAsync();
            return Ok(posts);
        }
    }
}
