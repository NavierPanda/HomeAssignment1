using System;
using System.Net;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;

namespace HomeAssignment.WebApi.Middlewares
{
    /// <summary>
    /// Middleware for exception handling
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// ctor
        /// </summary>
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 
        /// </summary>
        [UsedImplicitly]
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ArgumentException argumentException)
            {
                await HandleArgumentException(httpContext, argumentException);
            }

            catch (Exception)
            {
                SetEmptyResponse(httpContext, (int) HttpStatusCode.InternalServerError);
            }
        }

        private async Task HandleArgumentException(
            HttpContext httpContext,
            ArgumentException exception)
        {
            await WriteTextResponse(httpContext, (int) HttpStatusCode.BadRequest, exception.Message);
        }

        private async Task WriteTextResponse(
            HttpContext httpContext,
            int statusCode,
            string message)
        {
            httpContext.Response.Clear();
            httpContext.Response.ContentType = "text/plain";
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsync(message);
        }


        private static void SetEmptyResponse(HttpContext context, int statusCode)
        {
            context.Response.Clear();
            context.Response.StatusCode = statusCode;
        }
    }
}