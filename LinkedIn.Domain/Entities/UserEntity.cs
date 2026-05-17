using LinkedIn.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIn.Domain.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserRole Role { get; set; }
        public bool IsEmailVerified { get; set; } = false;
        public ProfileEntity Profile { get; set; }
        public ICollection<PostEntity> Posts { get; set; }
        public ICollection<CommentEntity> Comments { get; set; }
        public ICollection<LikeEntity> Likes { get; set; }
        public ICollection<RefreshTokenEntity> refreshTokens { get; set; }
        public ICollection<EmailVerificationCodeEntity> EmailVerificationCodes { get; set; }


    }
}
