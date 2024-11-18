using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibraryManagement.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            // Log the exception
            _logger.LogError(context.Exception, "An unhandled exception occurred during request processing.");

            // Set a custom response
            var response = new
            {
                Message = "An unexpected error occurred. Please try again later.",
                Error = context.Exception.Message 
            };

            context.Result = new JsonResult(response)
            {
                StatusCode = 500 // Internal Server Error
            };

            // Mark exception as handled
            context.ExceptionHandled = true;
        }
    }

}
