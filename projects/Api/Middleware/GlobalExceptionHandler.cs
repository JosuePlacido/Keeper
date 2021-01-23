using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Keeper.Api.Middlewares
{
	public class GlobalExceptionHandlerMiddleware : IMiddleware
	{
		private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

		public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
		{
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Unexpected error: {ex}");
				await HandleExceptionAsync(context, ex);
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			const int statusCode = StatusCodes.Status500InternalServerError;

			var json = JsonConvert.SerializeObject(new
			{
				statusCode,
				message = "Falha ao processar requisição",
				detailed = exception.Message
			});

			context.Response.StatusCode = statusCode;
			context.Response.ContentType = "application/problem+json";

			return context.Response.WriteAsync(json);
		}
	}
}
