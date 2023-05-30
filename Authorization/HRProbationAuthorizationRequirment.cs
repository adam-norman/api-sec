using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppSecTraining.Authorization
{
    public class HRProbationAuthorizationRequirment : IAuthorizationRequirement
    {
        public HRProbationAuthorizationRequirment(int periodInMonths)
        {
            PeriodInMonths = periodInMonths;
        }

        public int PeriodInMonths { get; }
    }
    public class HRProbationAuthorizationHandler : AuthorizationHandler<HRProbationAuthorizationRequirment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HRProbationAuthorizationRequirment requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "EmplymentDate"))
            {
                return Task.CompletedTask;
            }
            var emplymentDate = DateTime.Parse(context.User.Claims.First(c => c.Type == "EmplymentDate").Value);
            var period = DateTime.Now.Date - emplymentDate;
            if (period.Days >= requirement.PeriodInMonths * 30)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
