using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.IService;
using DormSearchBe.Domain.Dto.Prices;
using Microsoft.AspNetCore.Mvc;

namespace DormSearchBe.Api.Controllers.Prices
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricesController :ControllerBase
    {
        private readonly IPricesService _pricesService;
        public PricesController(IPricesService pricesService)
        {
            _pricesService = pricesService;
        }
        [HttpGet]
        public IActionResult GetAll([FromQuery] CommonListQuery query)
        {
            return Ok(_pricesService.Items(query));
        }

        [HttpPost]
        public IActionResult Create(PricesDto dto)
        {
            return Ok(_pricesService.Create(dto));
        }

        [HttpPatch("{id}")]
        public IActionResult Update(PricesDto dto)
        {
            return Ok(_pricesService.Update(dto));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return Ok(_pricesService?.Delete(id));
        }
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(_pricesService.GetById(id));
        }
        [HttpGet("ItemsList")]
        public IActionResult ItemsList()
        {
            return Ok(_pricesService.ItemsList());
        }
    }
}
