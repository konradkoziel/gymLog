using gymLog.API.Exceptions;
using gymLog.API.Services.interfaces;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace gymLog.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogService _log;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogService log)
        {
            _next = next;
            _log = log;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Unhandled exception");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    ValidationException => StatusCodes.Status400BadRequest,
                    UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                    _ => StatusCodes.Status500InternalServerError
                };

                var errorResponse = new
                {
                    error = ex.Message,
                    status = context.Response.StatusCode
                };

                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}
