using DormSearchBe.Application.IService;
using DormSearchBe.Domain.Dto.Auth;
using DormSearchBe.Domain.Dto.User;
using DormSearchBe.Domain.Repositories;
using DormSearchBe.Infrastructure.Exceptions;
using DormSearchBe.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

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
        [HttpPost("register")]
        public IActionResult SingUp([FromForm] Register singup)
        {
            var acoount = _userService.GetAll().Where(x => x.Email.Equals(singup.Email)).FirstOrDefault();
            if (acoount != null)
            {
                return Conflict(new { message = "Tài khoản đã tồn tại." });
            }
            var accountClient = new UserDto()
            {
                Email = singup.Email,
                Password = maHoaMatKhau(singup.Password),
                
            };
            if (_userService.Add(accountClient))
            {
                return Ok("Bạn đã đăng ký thành công");
            }
            return Conflict();
        }
        /* [HttpPost("register")]
         public IActionResult User_Register(Register dto)
         {
             return Ok(_userService.Register(dto));
         }*/
        private readonly string hash = @"foxle@rn";
        private string maHoaMatKhau(string text)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(text);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripleDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return Convert.ToBase64String(results);
                }
            }
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
                    FullName = username,
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
