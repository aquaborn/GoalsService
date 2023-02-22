using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Pet.Api.Authorization
{
    public class CustomAuthorizeAttribute
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // If "azp" claim do not exists - reject
            var auth0AppIdClaim = user.Claims.FirstOrDefault(c => c.Type == "azp");
            if (auth0AppIdClaim == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // If JWT produced through unauthorized Auth0 app - reject
            var services = context.HttpContext.RequestServices;
            var auth0Settings = services.GetService<Auth0Settings>();
            if (auth0Settings.AllowedAppId != auth0AppIdClaim.Value)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

        }
    }
