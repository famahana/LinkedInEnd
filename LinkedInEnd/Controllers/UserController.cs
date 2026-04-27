using LinkedIn.Application.DTOs.UserDto;
using LinkedIn.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkedIn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService _userService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
        [HttpGet("by-email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [Authorize]
        [HttpDelete("by-email/{email}")]
        public async Task<IActionResult> DeleteUserByEmail(string email)
        {
            var result = await _userService.DeleteUserByEmailAsync(email);
            return Ok(result);
        }
        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUserById(Guid id)
        {
            var result = await _userService.DeleteUserByIdAsync(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody]UserCreateDto dto)
        {
            string email = await _userService.AddUserAsync(dto);
            if(email == null)
            {
                return BadRequest(new { message = "User already exists" });
            }
            return CreatedAtAction(nameof(GetUserByEmail), new { email }, new
            {
                message = "Registration successful",
                email
            });


        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            var response = await _userService.LoginAsync(dto);
            if(response == null)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }
            return Ok(response);
        }



    }
}
