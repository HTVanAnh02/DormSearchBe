using DormSearchBe.Infrastructure.Enum;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Infrastructure.Common.Utilities
{
    public sealed class HasApprovalAttribute : AuthorizeAttribute
    {
        public HasApprovalAttribute(Approval approval) : base(policy: Convert.ToString(approval))
        {

        }
    }
}
