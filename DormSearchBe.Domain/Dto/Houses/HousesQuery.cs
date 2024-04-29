using DormSearchBe.Domain.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Domain.Dto.Houses
{
    public class HousesQuery:BaseEntity
    {
        public Guid HousesId { get; set; }
        public string? HousesName { get; set; }
        public string? Title { get; set; }
        public string? Interior { get; set; }
        public string? Acreage { get; set; }
        public string? AddressHouses { get; set; }
        public string? DateSubmitted { get; set; }
        public string? Photos { get; set; }
        public Guid? AreasId { get; set; }
        public string? AreasName { get; set; }
        public Guid? UsersId { get; set; }
        public string? UsersName { get; set; }
        public Guid? CityId { get; set; }
        public string? CityName { get; set; }
        public Guid? PriceId { get; set; }
        public string? Price {  get; set; }
        public Guid? RoomstyleId { get; set; }
        public string? RoomstyleName { get;set; }
        public Guid? FavoritesId {  get; set; }
        public bool IsFavorites { get; set; }
    }
}
