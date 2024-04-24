using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Infrastructure.Common.Utilities
{
    public class ApprovalRequirement : IAuthorizationRequirement
    {
        public ApprovalRequirement(string approvals)
        {
            Approval = approvals;
        }

        public string Approval { get; }
    }
}
