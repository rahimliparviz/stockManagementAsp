﻿using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Stock.Services.Errors;

namespace Stock.Api.Middlewares
{
    public class RestErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RestErrorHandlingMiddleware> _logger;

        public RestErrorHandlingMiddleware(RequestDelegate next,ILogger<RestErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        
        
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }
        }
        
        private async Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger<RestErrorHandlingMiddleware> logger)
        {
            object errors = null;

            switch (ex)
            {
                case RestException re:
                   logger.LogError(ex,"REST ERROR");
                   errors = re.Errors;
                   context.Response.StatusCode = (int)re.Code;
                    break;
                case Exception e:
                    logger.LogError(ex, "SERVER ERROR");
                    errors = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";
            if (errors != null)
            {
                //burda error obyektini json formatina cevirir ve WriteAsync ile throw edir deye bilerik yada terminala yazir
                var result = JsonSerializer.Serialize(new 
                {
                    errors
                });

                await context.Response.WriteAsync(result);
            }
        }
    }
}