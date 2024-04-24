using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.IService;
using DormSearchBe.Domain.Dto.City;
using Microsoft.AspNetCore.Mvc;

namespace DormSearchBe.Api.Controllers.City
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController :ControllerBase
    {
        private readonly ICityService _cityService;
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }
        [HttpGet]
        public IActionResult GetAll([FromQuery] CommonListQuery query)
        {
            return Ok(_cityService.Items(query));
        }

        [HttpPost]
        public IActionResult Create(CityDto dto)
        {
            return Ok(_cityService.Create(dto));
        }

        [HttpPatch("{id}")]
        public IActionResult Update(CityDto dto)
        {
            return Ok(_cityService.Update(dto));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return Ok(_cityService?.Delete(id));
        }
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(_cityService.GetById(id));
        }
        [HttpGet("ItemsList")]
        public IActionResult ItemsList()
        {
            return Ok(_cityService.ItemsList());
        }
    }
}
