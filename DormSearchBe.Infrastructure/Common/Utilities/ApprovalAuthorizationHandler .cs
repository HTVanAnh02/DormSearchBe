using DormSearchBe.Infrastructure.Settings;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Infrastructure.Common.Utilities
{
    public class ApprovalAuthorizationHandler : AuthorizationHandler<ApprovalRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApprovalRequirement requirement)
        {
            var approvals = context.User
                                     .Claims
                                     .Where(c => c.Type == CustomClaims.Approvals)
                                     .Select(c => c.Value);

            if (approvals.Contains(requirement.Approval))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
