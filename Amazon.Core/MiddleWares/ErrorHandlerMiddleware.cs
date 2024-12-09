using Amazon.Core.Bases;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security;
using System.Text.Json;

namespace Amazon.Core.MiddleWares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                Console.WriteLine($"Middleware: {error.Message}");

                var response = context.Response;

                response.ContentType = "application/json";
                var responseModel = new Response<string>()
                {
                    Succeeded = false,
                    Message = error?.Message
                };

                switch (error)
                {
                    // Unauthorized Access
                    case UnauthorizedAccessException e:
                        responseModel.StatusCode = HttpStatusCode.Unauthorized;
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        responseModel.Message = e.Message;
                        break;

                    // Validation Errors (data does not meet validation rules)
                    case ValidationException e:
                        responseModel.StatusCode = HttpStatusCode.UnprocessableEntity;
                        response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                        responseModel.Message = e.Message;
                        break;

                    // Not Found (e.g., resource does not exist)
                    case KeyNotFoundException e:
                        responseModel.StatusCode = HttpStatusCode.NotFound;
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        responseModel.Message = e.Message;
                        break;

                    // Database Update Error (e.g., SQL constraints or other DB-related issues)
                    case DbUpdateException e:
                        responseModel.StatusCode = HttpStatusCode.Conflict;
                        response.StatusCode = (int)HttpStatusCode.Conflict;
                        responseModel.Message = e.Message;
                        break;

                    // Conflict Error (e.g., concurrent updates)
                    case InvalidOperationException e:
                        responseModel.StatusCode = HttpStatusCode.Conflict;
                        response.StatusCode = (int)HttpStatusCode.Conflict;
                        responseModel.Message = e.Message;
                        break;

                    // Forbidden Access (e.g., insufficient permissions)
                    case SecurityException:
                        responseModel.StatusCode = HttpStatusCode.Forbidden;
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        responseModel.Message = "You do not have permission to access this resource.";
                        break;

                    // General System Error
                    case Exception:
                        responseModel.StatusCode = HttpStatusCode.InternalServerError;
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        responseModel.Message = "An unexpected error occurred. Please try again later.";
                        break;

                    // Fallback for unknown errors
                    default:
                        responseModel.StatusCode = HttpStatusCode.InternalServerError;
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        responseModel.Message = "An unhandled error occurred.";
                        break;
                }

                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);
            }
        }
    }
}
