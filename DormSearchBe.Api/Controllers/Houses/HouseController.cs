using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.IService;
using DormSearchBe.Domain.Dto.Houses;
using DormSearchBe.Infrastructure.Exceptions;
using DormSearchBe.Infrastructure.Settings;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DormSearchBe.Api.Controllers.Houses
{
    [Route("api/[controller]")]
    [ApiController]
    public class HousesController : ControllerBase
    {
        private readonly IHousesService _houseService;
        private readonly IUserService _usersService;
        public HousesController(IHousesService housesService, IUserService landlordsService)
        {
            _houseService = housesService;
            _usersService = landlordsService;
        }
        [HttpGet]
        public IActionResult GetAll([FromQuery] CommonListQuery query)
        {
            var objId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (objId == null)
            {
                throw new ApiException(HttpStatusCode.FORBIDDEN, HttpStatusMessages.Forbidden);
            }
            return Ok(_houseService.Items(query, Guid.Parse(objId)));
        }
        [HttpPost]
        public IActionResult Create(HousesDto dto)
        {
            var objId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (objId == null)
            {
                throw new ApiException(HttpStatusCode.FORBIDDEN, HttpStatusMessages.Forbidden);
            }
            var checkId = _usersService.ItemsList().Data.Where(x => x.UserId == Guid.Parse(objId)).FirstOrDefault();
            if (checkId == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, "Không tìm thấy thông tin nhà tuyển dụng");
            }

            dto.UserId = checkId.UserId;
            return Ok(_houseService.Create(dto));
        }
        [HttpPatch("{id}")]
        public IActionResult Update(HousesDto dto)
        {
            var objId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (objId == null)
            {

                throw new ApiException(HttpStatusCode.FORBIDDEN, HttpStatusMessages.Forbidden);
            }
            var checkId = _usersService.ItemsList().Data.Where(x => x.UserId == Guid.Parse(objId)).FirstOrDefault();
            if (checkId == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, "Không tìm thấy thông tin nhà tuyển dụng");
            }
            return Ok(_houseService.Update(dto));
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var objId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (objId == null)
            {
                throw new ApiException(HttpStatusCode.FORBIDDEN, HttpStatusMessages.Forbidden);
            }
            var checkId = _usersService.ItemsList().Data.Where(x => x.UserId == Guid.Parse(objId)).FirstOrDefault();
            if (checkId == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, "Không tìm thấy thông tin nhà tuyển dụng");
            }
            return Ok(_houseService?.Delete(id));
        }
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var objId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (objId == null)
            {
                throw new ApiException(HttpStatusCode.FORBIDDEN, HttpStatusMessages.Forbidden);
            }
            var checkId = _usersService.ItemsList().Data.Where(x => x.UserId == Guid.Parse(objId)).FirstOrDefault();
            if (checkId == null)
            {
                throw new ApiException(HttpStatusCode.ITEM_NOT_FOUND, "Không tìm thấy thông tin nhà tuyển dụng");
            }
            return Ok(_houseService.GetById(id));
        }
    }
}
