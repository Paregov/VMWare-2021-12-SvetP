using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WarehouseBackend.Models.ApiResponses;

namespace WarehouseBackend.Core.Exceptions
{
    public class ExceptionHandlerMiddleware
	{
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogger<ExceptionHandlerMiddleware> logger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                logger.LogError(e, "ExceptionHandlerMiddleware");

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                await ConvertException(context, e);
            }
        }

        private Task ConvertException(HttpContext context, Exception exception)
        {
            var response = new ErrorResponse()
            {
                ErrorCode = "500",
                Message = GetInnerException(exception).Message
            };

            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }

        public Exception GetInnerException(Exception exception)
        {
            if (exception.InnerException != null)
                return GetInnerException(exception.InnerException);

            return exception;
        }

        private readonly RequestDelegate _next;
	}
}
