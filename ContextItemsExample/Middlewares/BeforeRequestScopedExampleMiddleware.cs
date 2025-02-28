using ContextItemsExample.Models;
using Serilog;
using System.Text;
using System.Text.Json;

namespace ContextItemsExample.Middlewares
{
    public class BeforeRequestScopedExampleMiddleware
    {
        private readonly RequestDelegate _next;

        public BeforeRequestScopedExampleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // THIS IS AN EXAMPLE, NOT A GOOD PRACTICE TO READ THE BODY IN A MIDDLEWARE
            // Reading request body
            context.Request.EnableBuffering();

            using var reader = new StreamReader(context.Request.Body, encoding: Encoding.UTF8, leaveOpen: true);
            string requestBody = await reader.ReadToEndAsync();

            context.Request.Body.Position = 0;
            // Finish reading request body

            var body = JsonSerializer.Deserialize<LoginRequestModel>(requestBody);

            var session = new SessionData
            {
                SessionId = Guid.NewGuid(),
                Email = body.Email
            };

            Log.Information("-------------------------------------------------");
            Log.Information($"Setting Session Data in Before Request Middleware: {session.SessionId} - {session.Email}");

            context.Items.Add("Session", session);
            //UNCOMMENT THIS TO DEMONSTRATE THAT YOU CAN'T ADD AN ITEM WITH THE SAME KEY TWICE
            //context.Items.Add("Session", session);

            if (session.Email.ToLower().Contains("hotmail"))
                context.Items.Add("HotmailEmail", true);

            await _next(context);
        }
    }
}
