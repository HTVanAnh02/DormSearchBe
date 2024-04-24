using DormSearchBe.Domain.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Domain.Dto.Favorites
{
    public class FavoritesQuery : BaseEntity
    {
        public Guid FavoritesId { get; set; }
        public string FavoritesName { get; set; }
    }
}
