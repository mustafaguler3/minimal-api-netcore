namespace StudentEnrollment.Api.Filters
{
    public class LoggingFilter : IEndpointFilter
    {
        private readonly ILogger _logger;

        public LoggingFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<LoggingFilter>();
        }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var path = context.HttpContext.Request.Path;

            _logger.LogInformation("{method} request made to {path}",method,path);

            try
            {
                var results = await next(context);

                _logger.LogInformation("{method} request made to {path} successfull", method, path);

                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"{method} request to {path} failed",method,path);
                return Results.Problem("An error has occured,please try again later");
            }
        }
    }
}
