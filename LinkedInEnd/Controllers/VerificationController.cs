using LinkedIn.Application.DTOs.VerifyEmailDto;
using LinkedIn.Application.Interfaces.Services;
using LinkedIn.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkedIn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationController(IUserVerificationService _verificationService,IUserService _userService) : ControllerBase
    {
        [HttpPost("verify")]
        public async Task<IActionResult> VerifyEmail(VerifyEmailDto dto)
        {
            var authResult = await _userService.VerifyAndRefreshAsync(dto);
            if (authResult == null)
            {
                return BadRequest(new { message = "Wrong or expired code" });
            }
            return Ok(authResult);
        }
        [HttpPost("resend")]
        public async Task<IActionResult> ResendCode([FromBody]string email)
        {
            await _verificationService.GenerateAndSendCodeAsync(email);
            return Ok(new { message = "New code resend on your email" });
        }
    }
}
