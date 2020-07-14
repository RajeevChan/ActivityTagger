
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LogAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly TelemetryClient _telemetryClient;

        public ExceptionMiddleware(RequestDelegate next, TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
            _next = next;
        }

        public async Task<HttpContext> InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                _telemetryClient.TrackException(ex);
                await HandleExceptionAsync(httpContext, ex);
            }
            return httpContext;
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(exception.Message);
        }
    }
}
