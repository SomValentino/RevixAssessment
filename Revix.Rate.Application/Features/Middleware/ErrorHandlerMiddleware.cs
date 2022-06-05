namespace Revix.Rate.Application.Features.Middleware;

using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Revix.Rate.Domain.Models;

public class ErrorHandlerMiddleware {
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware (RequestDelegate next) {
        _next = next;
    }

    public async Task Invoke (HttpContext context) {
        try {
            await _next (context);
        } catch (Exception error) {
            var response = context.Response;
            response.ContentType = "application/json";

            switch (error) {
                case AppException e:
                    // custom application error
                    response.StatusCode = (int) HttpStatusCode.BadRequest;
                    break;
                default:
                    // unhandled error
                    response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize (new { message = error?.Message });
            await response.WriteAsync (result);
        }
    }
}