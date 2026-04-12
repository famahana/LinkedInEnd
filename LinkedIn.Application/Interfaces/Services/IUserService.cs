using LinkedIn.Application.DTOs.UserDto;
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
        Task<string> AddUserAsync(UserCreateDto dto);
        Task<string> DeleteUserByIdAsync(Guid id);
        Task<string> DeleteUserByEmailAsync(string email);
        Task<string> LoginAsync(UserLoginDto dto);
    }
}
