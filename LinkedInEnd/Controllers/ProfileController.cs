using LinkedIn.Application.DTOs.ProfileDto;
using LinkedIn.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkedIn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController(IProfileService _profileService) : ControllerBase
    {
        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetProfileByUserId(Guid userId)
        {
            var profile = await _profileService.GetProfileByUserIdAsync(userId);
            if(profile == null)
            {
                return NotFound();
            }
            return Ok(profile);
        }
        [HttpPost("{userId:guid}")]
        public async Task<IActionResult> AddProfile(Guid userId,ProfileCreateDto profile)
        {
            var profiles = await _profileService.AddProfileAsync(userId, profile);
            return Ok(profiles);
        }
        [HttpPut("{userId:guid}")]
        public async Task<IActionResult> Update(Guid userId,ProfileUpdateDto profile)
        {
            var profiles = await _profileService.UpdateProfileAsync(userId,profile);
            if(profiles == null)
            {
                return NotFound();
            }
            return Ok(profiles);
        }
        [HttpDelete("{userId:guid}")]
        public async Task<IActionResult> DeleteProfileById(Guid userId)
        {
            var deleted = await _profileService.DeleteProfileAsync(userId);
            if(!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }



    }
}
