using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace LibraryManagement.Filters
{
    public class LoggingFilter : ActionFilterAttribute
    {
        private readonly ILogger<LoggingFilter> _logger;

        public LoggingFilter(ILogger<LoggingFilter> logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = GetUsernameFromToken(context);
            var actionName = context.ActionDescriptor.DisplayName;
            var method = context.HttpContext.Request.Method;

            if (method == "POST")
            {
                _logger.LogInformation($"Employee creation initiated by {userId} in action {actionName} at {DateTime.UtcNow}.");
            }
            else if (method == "DELETE")
            {
                _logger.LogInformation($"Employee deletion initiated by {userId} in action {actionName} at {DateTime.UtcNow}.");
            }
            else if (method == "GET")
            {
                _logger.LogInformation($"Employee deletion initiated by {userId} in action {actionName} at {DateTime.UtcNow}.");
            }

            base.OnActionExecuting(context);
        }

        public string GetUsernameFromToken(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity is ClaimsIdentity identity && identity.IsAuthenticated)
            {
                var usernameClaim = identity.FindFirst(ClaimTypes.Name);
                return usernameClaim?.Value ?? "Anonymous";
            }

            return "UnauthenticatedUser";
        }

    }

}
