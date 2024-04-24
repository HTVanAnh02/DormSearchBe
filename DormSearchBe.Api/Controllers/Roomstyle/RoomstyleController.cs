using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.IService;
using DormSearchBe.Domain.Dto.Roomstyle;
using Microsoft.AspNetCore.Mvc;

namespace DormSearchBe.Api.Controllers.Roomstyle
{
    public class RoomstyleController :ControllerBase
    {
        private readonly IRoomstyleService _roomstyleService;
        public RoomstyleController(IRoomstyleService roomstyleService)
        {
            _roomstyleService = roomstyleService;
        }
        [HttpGet]
        public IActionResult GetAll([FromQuery] CommonListQuery query)
        {
            return Ok(_roomstyleService.Items(query));
        }

        [HttpPost]
        public IActionResult Create(RoomstyleDto dto)
        {
            return Ok(_roomstyleService.Create(dto));
        }

        [HttpPatch("{id}")]
        public IActionResult Update(RoomstyleDto dto)
        {
            return Ok(_roomstyleService.Update(dto));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return Ok(_roomstyleService?.Delete(id));
        }
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(_roomstyleService.GetById(id));
        }
        [HttpGet("ItemsList")]
        public IActionResult ItemsList()
        {
            return Ok(_roomstyleService.ItemsList());
        }
    }
}
