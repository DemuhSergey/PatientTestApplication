using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Patient.API.Models;
using Patient.Application.Common.Exceptions;

namespace Patient.API.Filters
{
    public class ApiExceptionFilterAttribute
    : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        public ApiExceptionFilterAttribute()
        {
            this._exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(ApplicationValidationException), this.HandleValidationException }
        };
        }

        public override void OnException(ExceptionContext context)
        {
            this.HandleException(context);
            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            var type = context.Exception.GetType();
            if (this._exceptionHandlers.ContainsKey(type))
            {
                this._exceptionHandlers[type].Invoke(context);
                return;
            }

            if (!context.ModelState.IsValid)
            {
                this.HandleInvalidModelStateException(context);
                return;
            }
        }

        private void HandleValidationException(ExceptionContext context)
        {
            var exception = (ApplicationValidationException)context.Exception;

            context.Result = new ObjectResult(new ApiValidationException(exception.Errors))
            {
                StatusCode = StatusCodes.Status400BadRequest
            };

            context.ExceptionHandled = true;
        }

        private void HandleInvalidModelStateException(ExceptionContext context)
        {
            var details = new ValidationProblemDetails(context.ModelState)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Result = new BadRequestObjectResult(details);
            context.ExceptionHandled = true;
        }
    }
}
