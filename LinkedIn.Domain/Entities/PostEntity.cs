using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIn.Domain.Entities
{
    public class PostEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
        public ICollection<CommentEntity> Comments { get; set; }
        public ICollection<LikeEntity> Likes { get; set; }
    }
}
