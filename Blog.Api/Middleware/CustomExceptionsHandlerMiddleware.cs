using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Blog.Application.Common.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Blog.Api.Middleware
{
    public class CustomExceptionsHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionsHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case ResourceNotFoundException resourceNotFoundException:
                        httpContext.Response.StatusCode = (int) HttpStatusCode.NotFound;
                        httpContext.Response.ContentType = "application/json";
                        await httpContext.Response.WriteAsync(
                            JsonSerializer.Serialize(new {error = resourceNotFoundException.Message}));
                        break;

                    case ValidationException validationException:
                        httpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                        httpContext.Response.ContentType = "application/json";
                        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(validationException.Errors));
                        break;

                    default: // Ignore all other exceptions
                        throw;
                }
            }
        }
    }

    public static class CustomExceptionsHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionsMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionsHandlerMiddleware>();
        }
    }
}