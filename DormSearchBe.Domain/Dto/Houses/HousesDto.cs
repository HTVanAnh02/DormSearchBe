using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Domain.Dto.Houses
{
    public class HousesDto
    {
        public Guid HousesId { get; set; }
        public string? HousesName { get; set; }
        public string? Title { get; set; }
        public string? Interior { get; set; }
        public string? Price { get; set; }
        public string? Acreage { get; set; }
        public string? AddressHouses { get; set; }
        public string? DateSubmitted { get; set; }
        public string? Photos { get; set; }
        public IFormFile? file { get; set; }
        public string? imageDelete { get; set; }
        public Guid? AreasId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? CityId { get; set; }
        public Guid? FavoritesId { get; set; }
        public Guid? RoomstyleId { get; set; }
    }
}
