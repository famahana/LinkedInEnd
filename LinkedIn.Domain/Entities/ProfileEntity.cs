using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIn.Domain.Entities
{
    public class ProfileEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Bio { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Location { get; set; }
        public string? Company { get; set; }
        public string? Position { get; set; }
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
    }
}
