using DormSearchBe.Domain.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Domain.Entity
{
    public class Approval:BaseEntity
    {
        public string? ApprovalId { get; set; }

        public string? ApprovalName { get; set; }
    }
}
