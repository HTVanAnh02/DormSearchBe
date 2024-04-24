using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Domain.Dto.Messages
{
    public class MessageDto
    {
        public Guid? MessagesId { get; set; }

        public string Message { get; set; }
    }
}
