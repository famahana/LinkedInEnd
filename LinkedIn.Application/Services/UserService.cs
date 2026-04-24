using AutoMapper;
using LinkedIn.Application.DTOs;
using LinkedIn.Application.DTOs.UserDto;
using LinkedIn.Application.Interfaces.Helpers;
using LinkedIn.Application.Interfaces.Repositories;
using LinkedIn.Application.Interfaces.Services;
using LinkedIn.Domain.Entities;
using LinkedIn.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIn.Application.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        private readonly IHashHelper _hashHelper;
        public UserService(IUserRepository userRepository,IMapper mapper,IJwtService jwtService,IHashHelper hashHelper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtService = jwtService;
            _hashHelper = hashHelper;    
        }

        public async Task<string> AddUserAsync(UserCreateDto dto)
        {
            var entity = _mapper.Map<UserEntity>(dto);
            dto.Email = dto.Email.Trim();
            entity.Role = UserRole.User;
            return await _userRepository.AddUserAsync(entity, dto.Password);
        }

        public async Task<string> DeleteUserByEmailAsync(string email)
        {
            return await _userRepository.DeleteUserByEmailAsync(email);
            
        }

        public async Task<string> DeleteUserByIdAsync(Guid id)
        {
            return await _userRepository.DeleteUserByIdAsync(id);
        }

        public async Task<ICollection<UserReadDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return _mapper.Map<ICollection<UserReadDto>>(users);
        }

        public async Task<UserReadDto> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            return _mapper.Map<UserReadDto>(user);
        }

        public async Task<UserReadDto> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return _mapper.Map<UserReadDto>(user);
        }

        public async Task<AuthResponseDto> LoginAsync(UserLoginDto dto)
        {
            dto.Email = dto.Email.Trim();
            var user = await _userRepository.GetUserByEmailAsync(dto.Email);
            if (user == null)
            {
                return null;
            }
            if (!_hashHelper.IsValidPassword(dto.Password, user.PasswordHash))
            {
                return null;

            }
            var token = _jwtService.GenerateAccessToken(user);
            return new AuthResponseDto
            {
                AccessToken = token,
                User = new UserReadDto
                {
                    Email = user.Email,
                    Role = user.Role.ToString(),
                    CreatedAt = user.CreatedAt
                }
            };
        }
    }
}
