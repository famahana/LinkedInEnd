using LinkedIn.Application.DTOs.VerifyEmailDto;
using LinkedIn.Application.Interfaces.Repositories;
using LinkedIn.Application.Interfaces.Services;
using LinkedIn.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIn.Application.Services
{
    public class UserVerificationService:IUserVerificationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailVerificationRepository _emailVerificationRepository;
        private readonly IEmailService _emailService;
        public UserVerificationService(IEmailService emailService,IUserRepository userRepository, IEmailVerificationRepository emailVerificationRepository)
        {
            _emailService = emailService;
            _userRepository = userRepository;
            _emailVerificationRepository = emailVerificationRepository;
        }

        public async Task GenerateAndSendCodeAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null || user.IsEmailVerified) return;
            var random = new Random();
            string code = random.Next(100000, 999999).ToString();
            var verificationCode = new EmailVerificationCodeEntity
            {
                Code = code,
                ExpiresAt = DateTime.UtcNow.AddMinutes(15),
                IsUsed = false,
                UserId = user.Id,
            };
            await _emailVerificationRepository.AddCodeAsync(verificationCode);
            string message = $"<h3>Welcome!</h3><p>Your code: <strong>{code}</strong></p><p>Code expires 15 minutes.</p>";
            await _emailService.SendEmailAsync(user.Email, "Confirm Email", message);
        }

        public async Task<bool> VerifyEmailAsync(VerifyEmailDto dto)
        {
            var user = await _userRepository.GetUserByEmailAsync(dto.Email);
            if (user == null) return false;
            var codeEntity = await _emailVerificationRepository.GetCodeByUserIdAsync(user.Id, dto.Code);
            if (codeEntity == null || codeEntity.ExpiresAt < DateTime.UtcNow)
            {
                return false;
                
            }
            codeEntity.IsUsed = true;
            await _emailVerificationRepository.UpdateCodeAsync(codeEntity);
            user.IsEmailVerified = true;
            await _userRepository.UpdateUserAsync(user);
            return true;
        }
    }
}
