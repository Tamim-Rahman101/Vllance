using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Vllance.Filters;

public class RoleAuthorizationAttribute : ActionFilterAttribute
{
    private readonly string[] _allowedRoles;

    public RoleAuthorizationAttribute(params string[] allowedRoles)
    {
        _allowedRoles = allowedRoles;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var httpContext = context.HttpContext;
        
        // Check if user is authenticated
        var userRole = httpContext.Session.GetString("UserRole");
        var userId = httpContext.Session.GetInt32("UserId");

        if (string.IsNullOrEmpty(userRole) || !userId.HasValue)
        {
            // User is not logged in, redirect to login
            context.Result = new RedirectToActionResult("Login", "Account", null);
            return;
        }

        // Check if user has the required role
        if (!_allowedRoles.Contains(userRole))
        {
            // User doesn't have permission, redirect to appropriate dashboard or access denied
            context.Result = new RedirectToActionResult("AccessDenied", "Account", null);
            return;
        }

        base.OnActionExecuting(context);
    }
}
