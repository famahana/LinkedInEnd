using LinkedIn.Application.DTOs;
using LinkedIn.Application.DTOs.RefreshTokenRequestDto;
using LinkedIn.Application.DTOs.UserDto;
using LinkedIn.Application.DTOs.VerifyEmailDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIn.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<ICollection<UserReadDto>> GetAllUsersAsync();
        Task<UserReadDto> GetUserByIdAsync(Guid id);
        Task<UserReadDto> GetUserByEmailAsync(string email);
        Task<AuthResponseDto> AddUserAsync(UserCreateDto dto);
        Task<string> DeleteUserByIdAsync(Guid id);
        Task<string> DeleteUserByEmailAsync(string email);
        Task<AuthResponseDto> LoginAsync(UserLoginDto dto,string IpAddress);
        Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequestDto dto, string IpAddress);
        Task<AuthResponseDto> VerifyAndRefreshAsync(VerifyEmailDto dto);



    }
}
