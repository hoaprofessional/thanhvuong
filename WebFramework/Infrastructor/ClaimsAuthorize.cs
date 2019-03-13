using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;
using WebFramework.Infrastructor;

public class ClaimRequirementAttribute : TypeFilterAttribute
{
    public ClaimRequirementAttribute(MyClaimType claimType, string claimValue) : base(typeof(ClaimRequirementFilter))
    {
        claimValue = "," + claimValue + ",";
        Arguments = new object[] { new Claim(claimType.ToString(), claimValue) };
    }
}

public class ClaimRequirementFilter : IAuthorizationFilter
{
    private readonly Claim _claim;

    public ClaimRequirementFilter(Claim claim)
    {
        _claim = claim;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        bool hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && _claim.Value.Contains("," + c.Value + ","));
        if (!hasClaim)
        {
            context.Result = new ForbidResult();
        }
    }
}