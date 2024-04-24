using DormSearchBe.Domain.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Domain.Entity
{
    public class Areas:BaseEntity
    {
        public Guid AreasId { get; set; }
        public string AreasName { get; set; }
        public ICollection<Houses>? Houses { get; set; }
    }
}
