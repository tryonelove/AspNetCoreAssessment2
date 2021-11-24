using Microsoft.AspNetCore.Builder;

namespace AspNetCoreAssessment2.Web.Middlewares
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseLogging(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LoggingMiddleware>();
        }
    }
}