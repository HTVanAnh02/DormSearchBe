using DormSearchBe.Application.IService;
using DormSearchBe.Domain.Dto.Auth;
using DormSearchBe.Domain.Dto.User;
using DormSearchBe.Domain.Repositories;
using DormSearchBe.Infrastructure.Exceptions;
using DormSearchBe.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DormSearchBe.Api.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public AuthController(IAuthService authService,IRefreshTokenRepository refreshTokenRepository, IUserService userService)
        {
            _authService = authService;
            _refreshTokenRepository = refreshTokenRepository;
            _userService = userService;
        }
        [HttpPost("Login")]
        public IActionResult Login(LoginDto dto)
        {
            return Ok(_authService.Login(dto));
        }
        [HttpPost("Refresh_token")]
        public IActionResult Refresh_token([FromBody] RefreshTokenSettings refreshToken)
        {
            var check = _refreshTokenRepository.GetAllData().Where(x => x.RefreshToken == refreshToken.Refresh_Token).FirstOrDefault();
            if (check != null)
            {
                return Ok(_authService.Refresh_Token(refreshToken));
            }
            var check1 = _refreshTokenRepository.GetAllData().Where(x => x.RefreshToken == refreshToken.Refresh_Token).FirstOrDefault();
            if (check1 != null)
            {
                return Ok(_userService.Refresh_Token(refreshToken));
            }
            throw new ApiException(HttpStatusCode.NOT_FOUND, HttpStatusMessages.NotFound);
        }
        [Authorize]
        [HttpPost("UserLogin")]
        public IActionResult User_Login(Login dto)
        {
            return Ok(_userService.Login(dto));
        }
        [Authorize]
        [HttpPost("UserRegister")]
        public IActionResult User_Register(Register dto)
        {
            return Ok(_userService.Register(dto));
        }
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            HttpContext.Request.Headers.Clear();
            HttpContext.Items.Clear();
            HttpContext.User = null;
            return Ok(new { success = true, data = "", statusCode = 200, message = "Đăng xuất thành công" });
        }
        [Authorize]
        [HttpGet("GetProfile")]
        public IActionResult GetProfile()
        {
            // Lấy thông tin người dùng từ HttpContext.User.
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var username = HttpContext.User.Identity.Name;
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var avatarUrl = ""; // Đường dẫn đến avatar, cần điều chỉnh tùy theo cách lưu trữ của bạn.

            // Kiểm tra xem các thông tin cần thiết đã được lấy chưa.
            if (userId != null && username != null && email != null)
            {
                // Tạo một đối tượng chứa thông tin cần trả về.
                var userProfile = new
                {
                    UserId = userId,
                    Username = username,
                    Email = email,
                    AvatarUrl = avatarUrl
                };

                return Ok(userProfile);
            }

            // Trường hợp không tìm thấy thông tin người dùng.
            throw new ApiException(HttpStatusCode.NOT_FOUND, HttpStatusMessages.NotFound);
        }
        [HttpPost("google-login")]
        public IActionResult GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            return Ok(_userService.UserLoginByGoole(request));
        }
        

    }
}
