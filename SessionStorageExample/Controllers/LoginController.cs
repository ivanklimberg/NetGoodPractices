using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Serilog;
using SessionStorageExample.DB;
using SessionStorageExample.Models;

namespace SessionStorageExample.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMemoryCache _cache;

        public LoginController(IMemoryCache cache)
        {
            _cache = cache;
        }

        [HttpPost]
        public async Task<IActionResult> PostLogin([FromBody] LoginRequestModel model)
        {
            var user = UsersList.Users.FirstOrDefault(x => x.Email == model.Email);

            var sessionData = _cache.Get<SessionData>(user.Id.ToString());

            Log.Information($"User: {sessionData.User.Id} - {sessionData.LoginCount}");

            return Ok(new
            {
                LoginCount = sessionData.LoginCount
            });
        }
    }
}
