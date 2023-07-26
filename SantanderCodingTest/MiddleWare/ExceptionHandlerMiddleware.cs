using SantanderCodingTest.Exceptions;

namespace SantanderCodingTest.MiddleWare
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
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
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error during executing");

                var response = context.Response;

                response.StatusCode = exception switch
                {
                    BadRequestException => 400,
                    _ => 500
                };

                if (response.StatusCode == 400)
                {
                    response.ContentType = "application/json";
                    await response.WriteAsync(@"{""errorMesage"" :" + @"""" + exception.Message + @"""}");
                }
                else
                {
                    await response.WriteAsync(exception.Message + exception.StackTrace);
                }
            }
        }
    }
}
