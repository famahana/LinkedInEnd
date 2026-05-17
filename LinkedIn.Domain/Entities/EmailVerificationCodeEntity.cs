using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIn.Domain.Entities
{
    public class EmailVerificationCodeEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; } 
        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; }
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }

    }
}
