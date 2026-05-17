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
    public class EmailVerificationRepository : IEmailVerificationRepository
    {
        private readonly LinkedInDbContext _context;
        public EmailVerificationRepository(LinkedInDbContext context)
        {
            _context = context;

        }
        public async Task AddCodeAsync(EmailVerificationCodeEntity codeEntity)
        {
            await _context.EmailVerificationCodes.AddAsync(codeEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<EmailVerificationCodeEntity?> GetCodeByUserIdAsync(Guid userId, string code)
        {
            return await _context.EmailVerificationCodes.FirstOrDefaultAsync(c=>c.UserId == userId && c.Code == code && !c.IsUsed);
        }

        public async Task UpdateCodeAsync(EmailVerificationCodeEntity codeEntity)
        {
            _context.EmailVerificationCodes.Update(codeEntity);
            await _context.SaveChangesAsync();
        }
    }
}
