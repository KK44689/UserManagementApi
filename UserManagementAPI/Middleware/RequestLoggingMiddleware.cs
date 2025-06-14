using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace UserManagementAPI.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var requestPath = context.Request.Path;
            var httpMethod = context.Request.Method;
            await _next(context);
            var statusCode = context.Response.StatusCode;
            _logger.LogInformation("HTTP {Method} {Path} responded {StatusCode}", httpMethod, requestPath, statusCode);
        }
    }
}
