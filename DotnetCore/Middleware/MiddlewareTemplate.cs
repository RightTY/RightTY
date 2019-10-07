using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DotnetCore.Middleware
{
#pragma warning disable 1591
    //Global Middleware
    public class GlobalMiddlewareTemplate
    {
        private readonly RequestDelegate _next;

        public GlobalMiddlewareTemplate(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync($"{nameof(GlobalMiddlewareTemplate)} in. \r\n");

            await _next(context);

            await context.Response.WriteAsync($"{nameof(GlobalMiddlewareTemplate)} out. \r\n");
        }
    }

    //Region Middleware
    public class RegionMiddlewareTemplate
    {
        private readonly RequestDelegate _next;

        public RegionMiddlewareTemplate(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync($"{nameof(RegionMiddlewareTemplate)} in. \r\n");

            await _next(context);

            await context.Response.WriteAsync($"{nameof(RegionMiddlewareTemplate)} out. \r\n");
        }
    }

    //Static Extensions Middleware

    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseFirstMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExtensionsMiddlewareTemplate>();
        }
    }
    public class ExtensionsMiddlewareTemplate
    {
        private readonly RequestDelegate _next;

        public ExtensionsMiddlewareTemplate(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync($"{nameof(ExtensionsMiddlewareTemplate)} in. \r\n");

            await _next(context);

            await context.Response.WriteAsync($"{nameof(ExtensionsMiddlewareTemplate)} out. \r\n");
        }
    }
}
#pragma warning restore 1591