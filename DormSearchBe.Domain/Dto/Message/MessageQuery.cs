using DormSearchBe.Domain.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Domain.Dto.Message
{
    public class MessageQuery :BaseEntity
    {
        public Guid? MessagesId { get; set; }

        public string Message { get; set; }
    }
}
