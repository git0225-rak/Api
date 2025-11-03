using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint
{
    //public class HeaderRemovalMiddleware
    //{
    //    private readonly RequestDelegate _next;

    //    public HeaderRemovalMiddleware(RequestDelegate next)
    //    {
    //        _next = next;
    //    }

    //    public async Task Invoke(HttpContext context)
    //    {
    //        context.Response.Headers.Remove("Access-Control-Allow-Origin");
    //        context.Response.Headers.Remove("Content-Length");
    //        context.Response.Headers.Remove("Content-Type");
    //        context.Response.Headers.Remove("Server");
    //        context.Response.Headers.Remove("X-Powered-By");
    //        context.Response.Headers.Remove("Content-Type");

    //        await _next(context);
    //    }
    //}
}
