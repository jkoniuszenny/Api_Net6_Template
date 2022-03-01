﻿using Application.NLog.Interfaces;
using Shared.GlobalResponse;
using System.Net;
using System.Text.Json;

namespace Api.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly INLogLogger _loggerManager;

        public ExceptionHandlerMiddleware(RequestDelegate next, INLogLogger loggerManager)
        {
            _next = next;
            _loggerManager = loggerManager;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }

        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.BadRequest;
            var exceptionType = exception.GetType();

            switch (exception)
            {
                case Exception when exceptionType == typeof(UnauthorizedAccessException):
                    statusCode = HttpStatusCode.Unauthorized;
                    break;
                case Exception when exceptionType == typeof(ArgumentException):
                    statusCode = HttpStatusCode.BadRequest;
                    break;
                case Exception when exceptionType == typeof(InvalidOperationException):
                    statusCode = HttpStatusCode.NotFound;
                    break;
                case Exception when exceptionType == typeof(Exception):
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            _loggerManager.LogError(exception.Message, exception);


            var payload = JsonSerializer.Serialize(Response<NullClass>.Fail((int)statusCode, exception.Message));

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsync(payload);
        }

    }
}
