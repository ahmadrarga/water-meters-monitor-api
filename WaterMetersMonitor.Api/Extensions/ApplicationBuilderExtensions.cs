using Microsoft.AspNetCore.Diagnostics;
using WaterMetersMonitor.Application.Exceptions;
using WaterMetersMonitor.Domain.Models;

namespace WaterMetersMonitor.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseApiExceptions(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = HandleExceptionsAsync
            });

            return app;
        }

        public static async Task HandleExceptionsAsync(HttpContext context)
        {
            var exception = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;
            if (exception is ApiException apiExc)
            {
                context.Response.StatusCode = (int) apiExc.StatusCode;
                await context.Response.WriteAsJsonAsync(new Error { ErrorCode = apiExc.ErrorCode, ErrorMessage = apiExc.Message });
            } else
            {
                await context.Response.WriteAsJsonAsync(new Error { ErrorCode = exception.GetType().Name, ErrorMessage = exception.Message });
            }
        }
    }
}
