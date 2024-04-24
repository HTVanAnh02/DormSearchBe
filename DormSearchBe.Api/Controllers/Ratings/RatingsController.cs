using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.IService;
using DormSearchBe.Domain.Dto.Ratings;
using DormSearchBe.Infrastructure.Exceptions;
using DormSearchBe.Infrastructure.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DormSearchBe.Api.Controllers.Ratings
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingsService _ratingsService;
        public RatingsController(IRatingsService ratingsService)
        {
            _ratingsService = ratingsService;
        }
        [HttpGet]
        public IActionResult GetAll([FromQuery] CommonListQuery query)
        {
            var objId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (objId == null)
            {
                throw new ApiException(HttpStatusCode.FORBIDDEN, HttpStatusMessages.Forbidden);
            }
            return Ok(_ratingsService.Items(query, Guid.Parse(objId)));
        }
        [HttpPost]
        public IActionResult Create(RatingsDto dto)
        {
            var objId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (objId == null)
            {
                throw new ApiException(HttpStatusCode.FORBIDDEN, HttpStatusMessages.Forbidden);
            }
            dto.StudentsId = Guid.Parse(objId);
            return Ok(_ratingsService.Create(dto));
        }

        [HttpPatch("{id}")]
        public IActionResult Update(RatingsDto dto)
        {
            return Ok(_ratingsService.Update(dto));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return Ok(_ratingsService?.Delete(id));
        }
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(_ratingsService.GetById(id));
        }
        [Authorize(Roles = "Landlord")]
        [HttpGet("ItemsByLandlord")]
        public IActionResult ItemsByLandlord([FromQuery] CommonQueryByHome query)
        {
            var objId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            if (objId == null)
            {
                throw new ApiException(HttpStatusCode.FORBIDDEN, HttpStatusMessages.Forbidden);
            }
            return Ok(_ratingsService.ItemsByUser(query, Guid.Parse(objId)));
        }
        [Authorize(Roles = "Landlord")]
        [HttpGet("ItemsByUseruitable")]
        public IActionResult ItemsByUseruitable([FromQuery] CommonQueryByHome query)
        {
            var objId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            if (objId == null)
            {
                throw new ApiException(HttpStatusCode.FORBIDDEN, HttpStatusMessages.Forbidden);
            }
            return Ok(_ratingsService.ItemsByUserSuitable(query, Guid.Parse(objId)));
        }
        [Authorize(Roles = "Students")]
        [HttpGet("ItemsByStudents")]
        public IActionResult ItemsByStudents([FromQuery] CommonListQuery query)
        {
            var objId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (objId == null)
            {
                throw new ApiException(HttpStatusCode.FORBIDDEN, HttpStatusMessages.Forbidden);
            }
            return Ok(_ratingsService.ItemsByStudents(query, Guid.Parse(objId)));
        }
        [HttpPatch("ChangeFeedback")]
        public IActionResult ChangeFeedback(RatingsChangeFeedback changeFeedback)
        {
            var objId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (objId == null)
            {
                throw new ApiException(HttpStatusCode.FORBIDDEN, HttpStatusMessages.Forbidden);
            }
            return Ok(_ratingsService.ChangeFeedback(changeFeedback));
        }
        [HttpPatch("ChangeStatus")]
        public IActionResult ChangeStatus(RatingsChangeStatus changeStatus)
        {
            var objId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (objId == null)
            {
                throw new ApiException(HttpStatusCode.FORBIDDEN, HttpStatusMessages.Forbidden);
            }
            return Ok(_ratingsService.ChangeStatus(changeStatus));
        }
    }
}
