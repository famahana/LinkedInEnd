using LinkedIn.Application.DTOs.UserDto;
using LinkedIn.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIn.Application.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateAccessToken(UserEntity user);
        RefreshTokenEntity GenerateRefreshToken(string ipAdress);
    }
}
