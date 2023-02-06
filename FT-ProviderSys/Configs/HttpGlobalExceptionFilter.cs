using FluentValidation;
using FT_ProviderSys.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace FT_ProviderSys.Configs
{
    // to know more about global exception handling ->  https://www.youtube.com/watch?v=_-9X8pqxOIo
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;

        public HttpGlobalExceptionFilter(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case ValidationException:
                    HandleValidationException(context);
                    break;
                case KeyNotFoundException or NotFoundException:
                    HandleNotFoundException(context);
                    break;
                case BadRequestException:
                    HandleBadRequestException(context);
                    break;
                default:
                    HandleGenericException(context);
                    break;
            }
        }

        private void HandleValidationException(ExceptionContext context)
        {
            var problemDetails = new ValidationProblemDetails
            {
                Instance = context.HttpContext.Request.Path,
                Status = StatusCodes.Status400BadRequest,
                Detail = "Please refer to the errors property for additional details."
            };

            var errors = new List<string> { context.Exception.Message };

            var validationErrors = ((ValidationException)context.Exception.GetBaseException()).Errors.ToList();

            errors.AddRange(validationErrors.Select(error => error.ErrorMessage));
            problemDetails.Errors.Add("DomainValidations", errors.ToArray());
            context.Result = new BadRequestObjectResult(problemDetails);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            var json = new JsonErrorResponse
            {
                Messages = new[] { context.Exception.Message }
            };

            if (_env.IsDevelopment())
                json.DeveloperMessage = context.Exception.Message;

            context.Result = new BadRequestObjectResult(json);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
        }

        private void HandleBadRequestException(ExceptionContext context)
        {
            var json = new JsonErrorResponse
            {
                Messages = new[] { context.Exception.Message }
            };

            if (_env.IsDevelopment())
                json.DeveloperMessage = context.Exception.Message;

            context.Result = new BadRequestObjectResult(json);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }

        private void HandleGenericException(ExceptionContext context)
        {
            var json = new JsonErrorResponse
            {
                Messages = new[] { context.Exception.Message }
            };

            if (_env.IsDevelopment()) json.DeveloperMessage = context.Exception;

            context.Result = new InternalServerErrorObjectResult(json);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }


        private class JsonErrorResponse
        {
            public string[] Messages { get; set; }
            public object DeveloperMessage { get; set; }
        }

        public class InternalServerErrorObjectResult : ObjectResult
        {
            public InternalServerErrorObjectResult(object error) : base(error)
            {
                StatusCode = StatusCodes.Status500InternalServerError;
            }
        }
    }
}
