using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;

namespace OzonEdu.MerchandiseServiceInfrastructure.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await LogRequest(context);
            await LogResponse(context);
        }

        private async Task LogRequest(HttpContext context)
        {
            context.Request.EnableBuffering();
            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);
            _logger.LogInformation($"Http Request Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} " +
                                   $"Host: {context.Request.Host} " +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString}\r\n " +
                                   $"Headers: {string.Join("\r\n    ",context.Request.Headers)}\r\n"+
                                   $"Request Body: {await new StreamReader(requestStream).ReadToEndAsync()}");
            context.Request.Body.Position = 0;
        }

        private async Task LogResponse(HttpContext context)
        {
            try
            {
                var originalBodyStream = context.Response.Body;
                await using var responseBody = _recyclableMemoryStreamManager.GetStream();
                context.Response.Body = responseBody;
                await _next(context);
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                _logger.LogInformation($"Http Response Information:{Environment.NewLine}" +
                                       $"Schema:{context.Request.Scheme} " +
                                       $"Host: {context.Request.Host} " +
                                       $"Path: {context.Request.Path} " +
                                       $"QueryString: {context.Request.QueryString} " +
                                       $"Response Body: {text}");
                await responseBody.CopyToAsync(originalBodyStream);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not log response body");
            }
        }
        
        
    }
}