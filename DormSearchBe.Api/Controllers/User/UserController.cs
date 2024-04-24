using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.IService;
using DormSearchBe.Domain.Dto.User;
using Microsoft.AspNetCore.Mvc;

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
