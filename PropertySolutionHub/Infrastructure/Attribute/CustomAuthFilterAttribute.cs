using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;

namespace PropertySolutionCustomerPortal.Infrastructure.Attribute
{
    public class CustomAuthFilterAttribute : TypeFilterAttribute
    {
        public CustomAuthFilterAttribute() : base(typeof(CustomAuthFilter))
        {
        }
    }

    public class CustomAuthFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;


            var standardAuth = context.ActionDescriptor.EndpointMetadata.OfType<AuthorizeAttribute>().Any();
            if (standardAuth)
                return;

            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            try
            {
                var token = context.HttpContext.Request.Headers["Authorization"].ToString().Substring("Bearer ".Length);
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                string role = jwt.Claims.First(c => c.Type == "authRole").Value;
                var validTill = jwt.ValidTo;

                if(validTill < DateTime.UtcNow || string.IsNullOrEmpty(role))
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

            }
            catch (Exception)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
