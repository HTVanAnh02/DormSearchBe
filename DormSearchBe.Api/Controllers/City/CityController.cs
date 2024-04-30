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
            // Kiểm tra xem tên thành phố đã tồn tại chưa
            var existingCity = _cityService.GetByName(dto.CityName);
            if (existingCity != null)
            {
                return Conflict("Tên thành phố đã tồn tại.");
            }

            // Tạo mới thành phố
            var result = _cityService.Create(dto);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public IActionResult Update(Guid id, CityDto dto)
        {
            // Lấy thông tin của thành phố hiện tại từ cơ sở dữ liệu
            var existingCity = _cityService.GetById(id);
            if (existingCity == null)
            {
                return NotFound("Không tìm thấy thành phố cần cập nhật.");
            }

            // Kiểm tra xem có thành phố nào khác có cùng tên không (trừ thành phố đang cập nhật)
            var otherCityWithSameName = _cityService.GetByName(dto.CityName);
            if (otherCityWithSameName != null && otherCityWithSameName != existingCity)
            {
                return Conflict("Tên thành phố đã tồn tại.");
            }

            // Cập nhật thành phố
            var result = _cityService.Update(dto);
            return Ok(result);
        }


        /*  [HttpPost]
          public IActionResult Create(CityDto dto)
          {
              return Ok(_cityService.Create(dto));
          }

          [HttpPatch("{id}")]
          public IActionResult Update(CityDto dto)
          {
              return Ok(_cityService.Update(dto));
          }*/

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
