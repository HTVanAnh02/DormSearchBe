using DormSearchBe.Domain.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Domain.Entity
{

    public class Roomstyle:BaseEntity
    {
        public Guid RoomstyleId { get; set; }
        public string? RoomstyleName { get; set; }
        public ICollection<Houses>? Houses { get; set; }

    }
}
