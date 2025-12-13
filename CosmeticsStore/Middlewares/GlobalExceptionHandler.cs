using CosmeticsStore.Domain.Exceptions.Base;
using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics;
using Serilog;

namespace CosmeticsStore.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, Exception exception,
            CancellationToken cancellationToken)
        {
            LogException(exception);

            var (statusCode, title, detail) = MapExceptionToProblemInformation(exception);

            await Results.Problem(
                statusCode: statusCode,
                title: title,
                detail: detail,
                extensions: new Dictionary<string, object?>
                {
                    ["traceId"] = Activity.Current?.Id ?? httpContext.TraceIdentifier
                }).ExecuteAsync(httpContext);

            return true;
        }

        private void LogException(Exception exception)
        {
            if (exception is CustomException)
            {
                Log.Warning(exception, exception.Message);
            }
            else
            {
                Log.Error(exception, exception.Message);
            }
        }

        private static (int statusCode, string title, string detail)
            MapExceptionToProblemInformation(Exception exception)
        {
            if (exception is not CustomException customException)
            {
                return (
                    StatusCodes.Status500InternalServerError,
                    "Internal server error",
                    "Some internal error on the server occurred."
                );
            }

            return (
                customException switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    ConflictException => StatusCodes.Status409Conflict,
                    UnauthorizedException => StatusCodes.Status401Unauthorized,
                    BadRequestException => StatusCodes.Status400BadRequest,
                    ForbiddenException => StatusCodes.Status403Forbidden,
                    _ => StatusCodes.Status500InternalServerError
                },
                customException.Title,
                customException.Message
            );
        }
    }
}
