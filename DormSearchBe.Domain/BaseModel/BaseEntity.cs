using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Domain.BaseModel
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public string? createdBy { get; set; }
        public DateTime? createdAt { get; set; }
        public string? updatedBy { get; set; }
        public DateTime? updatedAt { get; set; }
        public string deletedBy { get; set; }
        public DateTime? deletedAt { get; set; }

    }
}
