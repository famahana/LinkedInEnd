using AutoMapper;
using LinkedIn.Application.DTOs;
using LinkedIn.Application.DTOs.ProfileDto;
using LinkedIn.Application.DTOs.RefreshTokenRequestDto;
using LinkedIn.Application.DTOs.UserDto;
using LinkedIn.Application.Interfaces.Helpers;
using LinkedIn.Application.Interfaces.Repositories;
using LinkedIn.Application.Interfaces.Services;
using LinkedIn.Domain.Entities;
using LinkedIn.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
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
        private readonly IProfileService _profileService;
        public UserService(IUserRepository userRepository,IMapper mapper,IJwtService jwtService,IHashHelper hashHelper,IProfileService profileService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtService = jwtService;
            _hashHelper = hashHelper;    
            _profileService = profileService;
        }

        public async Task<string> AddUserAsync(UserCreateDto dto)
        {
            var entity = _mapper.Map<UserEntity>(dto);
            dto.Email = dto.Email.Trim();
            entity.Role = UserRole.User;
            var user = await _userRepository.AddUserAsync(entity, dto.Password);
            if(user != null)
            {
                await _profileService.AddProfileAsync(user.Id, new ProfileCreateDto
                {
                    AvatarUrl = "https://localhost:7271/uploads/1247.png",
                    BannerUrl = "https://localhost:7271/uploads/images.jpg",
                    FirstName = "Your name",
                    LastName = "Your lastname",
                    Company = "Your company",
                    Position = "Your position",
                    Location = "Your location",
                    Bio = ""
                    
                });
                return user.Email;
            }
            return null;
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

        public async Task<AuthResponseDto> LoginAsync(UserLoginDto dto,string IpAddress)
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
            var refresh = _jwtService.GenerateRefreshToken(IpAddress);
            user.refreshTokens.Add(refresh);
            await _userRepository.UpdateUserAsync(user);

            return new AuthResponseDto
            {
                AccessToken = token,
                RefreshToken = refresh.Token,
                User = new UserReadDto
                {
                    Email = user.Email,
                    Role = user.Role.ToString(),
                    CreatedAt = user.CreatedAt
                }
            };

        }

        public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequestDto dto, string IpAddress)
        {

         
                var principal = _jwtService.GetPrincipalFromExpiredToken(dto.AccessToken);
                if (principal == null) return null;
                var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;        
                if (!Guid.TryParse(userIdClaim, out Guid userId)) return null;
                var user = await _userRepository.GetUserByIdAsync(userId);
                if (user == null) return null;
                var existingToken = user.refreshTokens.FirstOrDefault(t => t.Token == dto.RefreshToken);
  
                if (existingToken == null || existingToken.Expires < DateTime.UtcNow)return null;
                var newAccessToken = _jwtService.GenerateAccessToken(user);
                var newRefreshToken = _jwtService.GenerateRefreshToken(IpAddress);
                user.refreshTokens.Remove(existingToken);
                user.refreshTokens.Add(newRefreshToken);
                await _userRepository.UpdateUserAsync(user);
                return new AuthResponseDto
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken.Token,
                    User = _mapper.Map<UserReadDto>(user)
                };
            
            



        }
    }
}
