using LinkedIn.Application.DTOs.VerifyEmailDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIn.Application.Interfaces.Services
{
    public interface IUserVerificationService
    {
        Task GenerateAndSendCodeAsync(string email);
        Task<bool> VerifyEmailAsync(VerifyEmailDto dto);
    }
}
