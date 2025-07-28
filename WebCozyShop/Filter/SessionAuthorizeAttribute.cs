using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebCozyShop.Filter
{
    public class SessionAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var isLoggedIn = context.HttpContext.Session.GetString("UserId") != null;
            if (!isLoggedIn)
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
            }
        }
    }
}
