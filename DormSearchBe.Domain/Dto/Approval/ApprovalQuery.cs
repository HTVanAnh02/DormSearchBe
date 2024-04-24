using DormSearchBe.Domain.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Domain.Dto.Approval
{
    public class ApprovalQuery:BaseEntity
    {
        public string ApprovalId { get; set; }
        public string AppApprovalName { get; set; }
    }
}
