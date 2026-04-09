using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIn.Domain.Entities
{
    public class LikeEntity
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
        public int PostId { get; set; }
        public PostEntity Post { get; set; }
    }
}
