using DormSearchBe.Domain.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Domain.Entity
{
    public class Ratings:BaseEntity
    {
        public Guid RatingsId { get; set; }
        public string RatingsDateTime { get; set; }
        public Guid HousesId { get; set; }
        public Guid UserId { get; set; }
        public string? Content { get; set; }
        public bool? IsStatus { get; set; }
        public bool? IsFeedback { get; set; }
        public ICollection<Houses>? Houses { get; set; }
        public ICollection<User>? Users { get; set; }
        public Ratings()
        {
            IsStatus = false;
            IsFeedback = null;
        }

    }
}
