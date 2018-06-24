using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace SZPNUW.DBService
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseSessionRefresh(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RefreshSessionMiddleware>();
        }
    }
}
