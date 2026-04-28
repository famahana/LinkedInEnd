using LinkedIn.Application.DTOs.ProfileDto;
using LinkedIn.Application.Interfaces.Services;
using LinkedIn.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LinkedIn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController(IProfileService _profileService) : ControllerBase
    {
        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            var profile = await _profileService.GetProfileByUserIdAsync(Guid.Parse(userId));
            if (profile == null)
            {
                return NotFound(new { message = "Profile not found" });
            }
            return Ok(profile);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProfile(ProfileCreateDto profileCreateDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            var profile = await _profileService.AddProfileAsync(Guid.Parse(userId), profileCreateDto);
            return Ok(profile);
        }


        [HttpPut("me")]
        public async Task<IActionResult> UpdateMyProfile(ProfileUpdateDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            var updated = await _profileService.UpdateProfileAsync(Guid.Parse(userId), dto);
            if (updated == null)
            {
                return NotFound(new { message = "Profile not found" });
            }
            return Ok(updated);
        }
        [HttpPost("upload-avatar")]
        public async Task<IActionResult>UploadAvatar(IFormFile file)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            var updated = await _profileService.UpdateAvatarAsync(Guid.Parse(userId), file);
            return Ok(updated);
        }
        [HttpPost("upload-banner")]
        public async Task<IActionResult>UploadBanner(IFormFile file)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            var updated = await _profileService.UpdateBannerAsync(Guid.Parse(userId), file);
            return Ok(updated);
        }
    }
            
        
    
}
