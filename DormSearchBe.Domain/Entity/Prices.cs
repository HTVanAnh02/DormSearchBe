using DormSearchBe.Domain.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Domain.Entity
{
    public class Prices:BaseEntity
    {
        public Guid PriceId { get; set; }
        public string? Price { get; set; }
        public ICollection<Houses>? Houses { get; set; }
    }
}
