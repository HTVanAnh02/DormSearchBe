using DormSearchBe.Domain.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Domain.Entity
{
    public class Message :BaseEntity
    {
        public Guid? MessagesId { get; set; }   

        public string? Messages { get; set; }
        public Guid UserId { get; set; }
        public ICollection<User>? Users { get; set; }

    }
}
