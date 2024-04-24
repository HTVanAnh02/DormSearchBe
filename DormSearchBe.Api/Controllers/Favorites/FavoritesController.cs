using DormSearchBe.Application.Helpers;
using DormSearchBe.Application.IService;
using DormSearchBe.Domain.Dto.Favorites;
using Microsoft.AspNetCore.Mvc;

namespace DormSearchBe.Api.Controllers.Favorites
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoritesService _favoritesService;
        public FavoritesController(IFavoritesService favoritesService)
        {
            _favoritesService = favoritesService;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] CommonListQuery query)
        {
            return Ok(_favoritesService.Items(query));
        }

        [HttpPost]
        public IActionResult Create(FavoritesDto dto)
        {
            return Ok(_favoritesService.Create(dto));
        }

        [HttpPatch("{id}")]
        public IActionResult Update(FavoritesDto dto)
        {
            return Ok(_favoritesService.Update(dto));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return Ok(_favoritesService?.Delete(id));
        }
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(_favoritesService.GetById(id));
        }
        [HttpGet("ItemsList")]
        public IActionResult ItemsList()
        {
            return Ok(_favoritesService.ItemsList());
        }
    }
}
