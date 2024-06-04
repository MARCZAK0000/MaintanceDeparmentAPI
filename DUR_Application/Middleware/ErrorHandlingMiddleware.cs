using DUR_Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace DUR_Application.Middleware
{
    public class ErrorHandlingMiddleware:IMiddleware
    {
        private readonly ILogger _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.ContentType = "application/json";

                var problemDetails = new ProblemDetails()
                {
                    Title = "Not Found",
                    Type = "Empty Response",
                    Status = (int)HttpStatusCode.NotFound,
                    Detail = ex.Message
                };
                var errorResponse = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(errorResponse);
            }

            catch (ForbidenException ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                context.Response.ContentType = "application/json";

                var problemDetails = new ProblemDetails()
                {
                    Title = "Forbiden",
                    Type = "No accesc",
                    Status = (int)HttpStatusCode.NotFound,
                    Detail = ex.Message
                };
                var errorResponse = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(errorResponse);

            }

            catch (UnauthorizedException ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Response.ContentType = "application/json";

                var problemDetails = new ProblemDetails()
                {
                    Title = "Unauthorized",
                    Type = "No authentication",
                    Status = (int)HttpStatusCode.Unauthorized,
                    Detail = ex.Message
                };
                var errorResponse = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(errorResponse);
            }

            catch (BadRequestException ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var problemDetails = new ProblemDetails()
                {
                    Title = "Bad Request",
                    Type = "Wrong Request",
                    Status = (int)HttpStatusCode.BadRequest,
                    Detail = ex.Message
                };
                var errorResponse = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(errorResponse);
            }

            catch(WrongDataException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.Conflict; 
                context.Response.ContentType = "application/json";
                var problemDetails = new ProblemDetails()
                {
                    Title = "Wrong data",
                    Type = "Conflict",
                    Status = (int)HttpStatusCode.Conflict,
                    Detail = ex.Message
                };

                var errorResponse = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(errorResponse);
            }
            catch (Exception ex )
            {

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var problemDetails = new ProblemDetails()
                {
                    Title = "Internal Server Error",
                    Type = "Server Error",
                    Status = (int)HttpStatusCode.InternalServerError,
                    Detail = ex.Message
                };
                var response =JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(response);
            }
        }
    }
}
