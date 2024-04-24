using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Domain.Dto.User
{
    public class TokenDto
    {
        public string? AccessToken { get; set; }
        public int AccessTokenExpiration { get; set; }
        public string? RefreshToken { get; set; }
        public int RefreshTokenExpiration { get; set; }
        public string? Role { get; set; }
    }
}
