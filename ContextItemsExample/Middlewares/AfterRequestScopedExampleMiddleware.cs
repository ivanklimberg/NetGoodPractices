using ContextItemsExample.Models;
using Serilog;

namespace ContextItemsExample.Middlewares
{
    public class AfterRequestScopedExampleMiddleware
    {
        private readonly RequestDelegate _next;

        public AfterRequestScopedExampleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            var session = (SessionData)context.Items["Session"];

            Log.Information($"Getting Session Data in After Request Middleware: {session.SessionId} - {session.Email}");
            Log.Information("-------------------------------------------------");


        }
    }
}
