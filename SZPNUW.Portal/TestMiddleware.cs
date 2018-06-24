using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SZPNUW.Portal
{
    public class TestMiddleware
    {
        private readonly RequestDelegate next;

        public TestMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                //context.Request.PathBase = "/portal";
                await next(context);
            }
            catch (Exception ex)
            {
                context.Response.WriteAsync(string.Empty);
            }
        }
    }

    public static class TestMiddlewareExtensions
    {
        public static IApplicationBuilder UseTest(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TestMiddleware>();
        }
    }
}
