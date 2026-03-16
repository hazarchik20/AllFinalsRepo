using FinalProjectASP_Net.Middleware;

namespace FinalProjectASP_Net.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder app)
        {
            // Here you can add your Middleware, for example:
            // app.UseMiddleware<MyCustomMiddleware>();

            app.UseMiddleware<GlobalExceptionHandler>();
            app.UseMiddleware<RequestLoggingMiddleware>();
            return app;

        }
    }
}
