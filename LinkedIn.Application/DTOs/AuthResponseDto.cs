using LinkedIn.Application.DTOs.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIn.Application.DTOs
{
    public class AuthResponseDto
    {
        public string AccessToken {  get; set; }
        public UserReadDto User { get; set; }
    }
}
