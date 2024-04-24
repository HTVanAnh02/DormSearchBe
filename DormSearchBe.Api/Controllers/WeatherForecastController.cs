
using DormSearchBe.Application.Helpers;
using DormSearchBe.Infrastructure.Common.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DormSearchBe.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        [Authorize]
       /* [HasApproval(Approval.Modify)]*/
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        /*[HasApproval(Approval.Modify)]*/
        [HttpGet("Users")]
        public IActionResult GetUser([FromQuery] CommonListQuery query)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(userId);

        }
        [HttpPost("upload")]
        public IActionResult UploadFile(IFormFile file)
        {
            var url = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            var dto = FileUploadService.CreatePDF(file);
            return Ok(new { url, dto });
        }
    }
}