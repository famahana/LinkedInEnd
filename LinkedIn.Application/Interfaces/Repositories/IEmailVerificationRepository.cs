using LinkedIn.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIn.Application.Interfaces.Repositories
{
    public interface IEmailVerificationRepository
    {
        Task AddCodeAsync(EmailVerificationCodeEntity codeEntity);
        Task<EmailVerificationCodeEntity?> GetCodeByUserIdAsync(Guid userId,string code);
        Task UpdateCodeAsync(EmailVerificationCodeEntity codeEntity);
    }
}
