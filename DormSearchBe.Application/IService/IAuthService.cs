using DormSearchBe.Application.Wrappers.Concrete;
using DormSearchBe.Domain.Dto.Auth;
using DormSearchBe.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Application.IService
{
    public interface IAuthService
    {
        DataResponse<TokenDto> Login(LoginDto dto);
        DataResponse<TokenDto> Refresh_Token(RefreshTokenSettings token);
    }
}
