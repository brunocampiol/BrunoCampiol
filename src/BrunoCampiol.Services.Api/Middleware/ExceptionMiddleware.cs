using BrunoCampiol.CrossCutting.Common.Common;

namespace BrunoCampiol.Services.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(HttpContext httpContext, ILogger<ExceptionMiddleware> logger)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                HandleException(ex, logger);
                throw;
            }
        }

        private void HandleException(Exception exception, ILogger<ExceptionMiddleware> logger)
        {
            try
            {
                logger.LogError(exception, exception.AllExceptionMessages());
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, ex.AllExceptionMessages());
            }
        }
    }
}
