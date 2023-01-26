using Newtonsoft.Json;

namespace Pokedex.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IHostEnvironment environment)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(context, environment, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, IHostEnvironment  environment, Exception exception)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            if (!environment.IsProduction())
            {
                context.Response.ContentType= "application/json";

                var result = JsonConvert.SerializeObject(new
                {
                    Error = exception.Message,
                    Details = exception.InnerException?.Message
                });

                await context.Response.WriteAsync(result);
            }
        }
    }
}
