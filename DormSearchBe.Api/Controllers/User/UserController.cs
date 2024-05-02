using CloudinaryDotNet.Core;
using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.IService;
using DormSearchBe.Domain.Dto.User;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace DormSearchBe.Api.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] CommonListQuery query)
        {
            return Ok(_userService.Items(query));
        }

        [HttpPost]
        public IActionResult Create([FromForm] UserDto dto)
        {
            return Ok(_userService.Create(dto));
        }

        [HttpPatch("{id}")]
        public IActionResult Update([FromForm] UserDto dto)
        {
            return Ok(_userService.Update(dto));
        }
        [HttpPost("DoiMatKhau")]
        public IActionResult DoiMatKhau(DoiMatKhau model)
        {
            var acoount = _userService.GetAll().Where(x => x.Email.Equals(model.Email)).FirstOrDefault();
            if (acoount == null)
            {
                return BadRequest("tài khoản ko có");
            }
            acoount.Password = maHoaMatKhau(model.NewPass);
            if (_userService.UpdatePass(acoount))
            {
                return Ok("Thành công. Vui lòng bạn đăng nhập");
            }
            else
            {
                return BadRequest();
            }
        }
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
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return Ok(_userService?.Delete(id));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(_userService.GetById(id));
        }
    }
}
