using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.IService;
using Microsoft.AspNetCore.Mvc;

namespace DormSearchBe.Api.Controllers.Home
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHousesService _houseService;
        public HomeController(IHousesService jobService)
        {
            _houseService = jobService;
        }
        [HttpGet]
        public IActionResult ListJob([FromQuery] CommonQueryByHome queryByHome)
        {
            return Ok(_houseService.ItemsByHome(queryByHome));
        }
        [HttpGet("ItemById/{id}")]
        public IActionResult ItemById(Guid id)
        {
            return Ok(_houseService.ItemById(id));
        }
        

    }
}
