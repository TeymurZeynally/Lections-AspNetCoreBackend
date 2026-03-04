
namespace Lecture05.Middlewares.Middlewares
{
    internal sealed class RickRollingMiddleware(SomeDependency someDependency) : IMiddleware
    {
        private const string RickRollUrl = "https://rutube.ru/video/25e4691cbe14b5b7521732b40b691025/";

        private int _requestCount = 0;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            someDependency.DoSomething();

            Interlocked.Increment(ref _requestCount);

            if (_requestCount % 5 == 0)
            {
                context.Response.StatusCode = StatusCodes.Status302Found;
                context.Response.Headers.Location = RickRollUrl;

                return;
            }

            await next(context);
        }
    }
}
