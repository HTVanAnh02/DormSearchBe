using DormSearchBe.Domain.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Domain.Entity
{
    public class Favorites:BaseEntity
    {
        public Guid FavoritesId {  get; set; }
        public string FavoritesName { get; set; }
        public Guid HousesId { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public ICollection<Houses>? Houses { get; set; }
    }
}
