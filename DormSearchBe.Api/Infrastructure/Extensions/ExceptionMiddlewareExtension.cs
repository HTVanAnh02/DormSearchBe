using DormSearchBe.Api.Infrastructure.Middleware;

namespace DormSearchBe.Api.Infrastructure.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        public static void UseCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
