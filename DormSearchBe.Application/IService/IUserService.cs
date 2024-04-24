using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.Wrappers.Concrete;
using DormSearchBe.Domain.Dto.User;
using DormSearchBe.Domain.Entity;
using DormSearchBe.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Application.IService
{
    public interface IUserService
    {
        PagedDataResponse<UserQuery> Items(CommonListQuery commonList);
        DataResponse<List<UserDto>> ItemsList();
        DataResponse<UserQuery> Create(UserDto dto);
        DataResponse<UserQuery> Update(UserDto dto);
        DataResponse<UserQuery> Delete(Guid id);
        DataResponse<UserQuery> GetById(Guid id);
        DataResponse<TokenDto> Login(Login dto);
        DataResponse<TokenDto> Refresh_Token(RefreshTokenSettings token);
        DataResponse<TokenDto> UserLoginByGoole(GoogleLoginRequest request);
        List<Approval> GetUserApproval(Guid id);
    }
}
