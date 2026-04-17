using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIn.Application.DTOs.ProfileDto
{
    public class ProfileReadDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Bio {  get; set; }
        
        public string? AvatarUrl { get; set; }
        public string? Location { get; set; }

    }
}
