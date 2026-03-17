namespace FinalProjectASP_Net.Middleware
{
    public class RequestLoggingMiddleware : IMiddleware
    {
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(ILogger<RequestLoggingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var start = DateTime.UtcNow;

            await next(context);

            var duration = DateTime.UtcNow - start;

            _logger.LogInformation(
                "HTTP {Method} {Path} responded {StatusCode} in {Duration} ms",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                duration.TotalMilliseconds);
        }
    }
}
