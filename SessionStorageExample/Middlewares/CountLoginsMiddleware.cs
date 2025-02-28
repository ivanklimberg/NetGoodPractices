using Microsoft.Extensions.Caching.Memory;
using SessionStorageExample.DB;
using SessionStorageExample.Models;
using System.Text;
using System.Text.Json;

namespace SessionStorageExample.Middlewares
{
    public class CountLoginsMiddleware
    {
        private readonly RequestDelegate _next;
        private const int TIMEOUT_MINUTES = 15;

        public CountLoginsMiddleware(RequestDelegate next)
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

            var user = UsersList.Users.FirstOrDefault(x => x.Email == body.Email);

            if (user != null)
            {
                var cache = context.RequestServices.GetRequiredService<IMemoryCache>();

                var sessionData = cache.Get<SessionData>(user.Id.ToString());
                if (sessionData == null)
                {
                    cache.Set(user.Id.ToString(), new SessionData { LoginCount = 1, User = user }, TimeSpan.FromMinutes(TIMEOUT_MINUTES));
                }
                else
                {
                    sessionData.LoginCount++;
                    cache.Set(user.Id.ToString(), sessionData, TimeSpan.FromMinutes(TIMEOUT_MINUTES));
                }
            }

            await _next(context);
        }
    }
}
