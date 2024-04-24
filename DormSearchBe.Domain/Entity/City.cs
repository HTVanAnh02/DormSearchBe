using DormSearchBe.Domain.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Domain.Entity
{
    public class City : BaseEntity
    {
        public Guid CityId { get; set; }
        public string? CityName { get; set; }
        public ICollection<Houses>? Houses { get; set; }

    }
}
