using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormSearchBe.Infrastructure.Common.Utilities
{
    public class ApprovalAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        public ApprovalAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
        {
        }

        public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            var policy = await base.GetPolicyAsync(policyName);

            if (policy is not null)
                return policy;

            return new AuthorizationPolicyBuilder()
                       .AddRequirements(new ApprovalRequirement(policyName))
                       .Build();
        }
    }
}

