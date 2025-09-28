namespace eCommerce.API.Middlewares;

// This is extention class of the middleware
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            // Log exception type and message
            _logger.LogError($"{ex.GetType().ToString()}: {ex.Message}");
            
            if(ex.InnerException is not null)
            {
                _logger.LogError($"{ex.InnerException.GetType().ToString()}: {ex.InnerException.Message}");
            }

            httpContext.Response.StatusCode = 500; // Internal server error
            await httpContext.Response.WriteAsJsonAsync(new {Message = ex.Message, Type = ex.GetType().ToString()});
        }
    }
}

// Extension method used to add the middleware to the http request pipeline
public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
    {
        return
            builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
