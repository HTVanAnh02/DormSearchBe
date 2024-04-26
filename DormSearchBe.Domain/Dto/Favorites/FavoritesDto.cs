using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Domain.Dto.Favorites
{
    public class FavoritesDto
    {
        public Guid FavoritesId { get; set; }
        public Guid UserId { get; set; }
        public bool IsFavorites { get; set; }
        public Guid HousesId { get; set; }
    }
}
