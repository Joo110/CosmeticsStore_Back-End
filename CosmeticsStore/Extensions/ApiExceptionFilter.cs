using CosmeticsStore.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CosmeticsStore.Extensions
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is CartNotFoundException cartEx)
            {
                context.Result = new NotFoundObjectResult(new { message = cartEx.Message });
                context.ExceptionHandled = true;
                return;
            }

            context.Result = new ObjectResult(new
            {
                message = "An unexpected error occurred.",
                detail = context.Exception.Message
            })
            {
                StatusCode = 500
            };
            context.ExceptionHandled = true;
        }
    }
}
