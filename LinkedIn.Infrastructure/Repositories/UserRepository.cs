using LinkedIn.Application.Interfaces.Helpers;
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
    public class UserRepository:IUserRepository
    {
        private readonly LinkedInDbContext _linkedInDbContext;
        private readonly IHashHelper _hashHelper;

        public UserRepository(LinkedInDbContext context,IHashHelper hashHelper)
        {
            _linkedInDbContext = context;
            _hashHelper = hashHelper;
        }

        public async Task<string> AddUserAsync(UserEntity user, string password)
        {
            user.PasswordHash = _hashHelper.Hash(password);
            await _linkedInDbContext.Users.AddAsync(user);
            await _linkedInDbContext.SaveChangesAsync();
            return user.Email;

        }

        public async Task<string> DeleteUserByEmailAsync(string email)
        {
            var user = await _linkedInDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return null;
            }
            _linkedInDbContext.Users.Remove(user);
            await _linkedInDbContext.SaveChangesAsync();
            return user.Email;
        }

        public async Task<string> DeleteUserByGuidAsync(Guid id)
        {
            var user = await _linkedInDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return null;
            }
            _linkedInDbContext.Users.Remove(user);
            await _linkedInDbContext.SaveChangesAsync();
            return user.Email;
        }

        public async Task<ICollection<UserEntity>> GetAllUsersAsync()
        {
            return await _linkedInDbContext.Users.ToArrayAsync();
        }

        public async Task<UserEntity> GetUserByEmailAsync(string email)
        {
            return await _linkedInDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UserEntity> GetUserByIdAsync(Guid id)
        {
            return await _linkedInDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
