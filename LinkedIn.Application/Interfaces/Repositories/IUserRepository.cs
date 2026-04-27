using LinkedIn.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIn.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<ICollection<UserEntity>> GetAllUsersAsync();
        Task<UserEntity> GetUserByIdAsync(Guid id);
        Task<UserEntity> GetUserByEmailAsync(string email);
        Task<UserEntity> AddUserAsync(UserEntity user,string password);
        Task<string> DeleteUserByIdAsync(Guid id);
        Task<string> DeleteUserByEmailAsync(string email);
        
    }
}
