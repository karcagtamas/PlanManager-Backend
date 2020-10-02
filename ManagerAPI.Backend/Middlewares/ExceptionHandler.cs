﻿using System;
using System.Net;
using System.Threading.Tasks;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ManagerAPI.Backend.Middlewares
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        private ILoggerService _loggerService;
        private const string FatalError = "Something bad happened. Try againg later";

        public ExceptionHandler(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context, ILoggerService logger)
        {
            this._loggerService = logger;
            try
            {
                await _next.Invoke(context);
            }
            catch (MessageException me)
            {
                await HandleExceptionAsync(context, me).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e).ConfigureAwait(false);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            const int statusCode = (int) HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(
                this._loggerService.ExceptionToResponse(
                    exception is MessageException me ? me : new Exception(FatalError), exception));
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }

        private Task HandleExceptionAsync(HttpContext context, MessageException exception)
        {
            context.Response.ContentType = "application/json";
            const int statusCode = (int) HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(this._loggerService.ExceptionToResponse(exception));
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}