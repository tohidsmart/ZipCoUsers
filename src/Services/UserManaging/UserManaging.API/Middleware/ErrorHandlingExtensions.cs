using System;
using System.Threading.Tasks;
using UserManaging.CQRS.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace UserManaging.API.Middleware
{
    public static class ErrorHandlingExtensions
    {
        public static Task HandleExceptionAsync(this Exception exception, HttpContext context)
        {
            context.Response.ContentType = "application/json";

            if (exception is NotFoundException)
            {
                var notFoundException = exception as NotFoundException;
                context.Response.StatusCode = (int)notFoundException.HttpStatusCode;
                return context.Response.WriteAsync(exception.Message);
            }
            else if (exception is BadRequestException)
            {
                var badRequestExcpetion = exception as BadRequestException;
                context.Response.StatusCode = (int)badRequestExcpetion.HttpStatusCode;
                return context.Response.WriteAsync(exception.Message);
            }
            else if (exception is DbUpdateException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                string message = $"{exception.Message}\n{exception?.InnerException?.Message}";
                return context.Response.WriteAsync(message ?? "some error has happend during database operation");
            }
            else
            {
                var errorResponse = context.GenerateInternalServerExceptionResponse(exception);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return context.Response.WriteAsync(errorResponse);
            }
        }
        private static string GenerateInternalServerExceptionResponse(this HttpContext context, Exception exception)
        {
            string body = string.Empty;
            using (StreamReader stream = new StreamReader(context.Request?.Body))
            {
                body = stream.ReadToEndAsync().GetAwaiter().GetResult();
            }
            var exceptionResponse = new
            {
                path = context.Request.Path,
                method = context.Request.Method,
                message = exception?.Message,
                innerMessage = exception?.InnerException?.Message,
                exceptionType = exception.GetType().Name
            };

            var jsonException = JsonSerializer.Serialize(exceptionResponse,
                                  new JsonSerializerOptions { WriteIndented = true });
            return jsonException;

        }

    }
}
