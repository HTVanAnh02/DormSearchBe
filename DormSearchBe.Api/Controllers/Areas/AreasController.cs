using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.IService;
using DormSearchBe.Domain.Dto.Areas;
using Microsoft.AspNetCore.Mvc;

namespace DormSearchBe.Api.Controllers.Areas
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreasController :ControllerBase
    {
        private readonly IAreasService _areasService;
        public AreasController(IAreasService AreasService)
        {
            _areasService = AreasService;
        }
        [HttpGet]
        public IActionResult GetAll([FromQuery] CommonListQuery query)
        {
            return Ok(_areasService.Items(query));
        }

        [HttpPost]
        public IActionResult Create(AreasDto dto)
        {
            return Ok(_areasService.Create(dto));
        }

        [HttpPatch("{id}")]
        public IActionResult Update(AreasDto dto)
        {
            return Ok(_areasService.Update(dto));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return Ok(_areasService?.Delete(id));
        }
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(_areasService.GetById(id));
        }
        [HttpGet("ItemsList")]
        public IActionResult ItemsList()
        {
            return Ok(_areasService.ItemsList());
        }
    }
}
