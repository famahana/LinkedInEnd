using LinkedIn.Application.DTOs.RefreshTokenRequestDto;
using LinkedIn.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkedIn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IUserService _userService) : ControllerBase
    {
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenRequestDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new {message = "Invalid request" });
            }
            string ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            var result = await _userService.RefreshTokenAsync(dto, ipAddress);
            if(result == null)
            {
                return Unauthorized(new { message = "Invalid token" });
            }
            return Ok(result);


        }


    }
}
