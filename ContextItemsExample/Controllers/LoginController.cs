using ContextItemsExample.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ContextItemsExample.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> PostLogin([FromBody] LoginRequestModel model)
        {
            var session = (SessionData)HttpContext.Items["Session"];

            Log.Information($"Session Data in Controller: {session.SessionId} - {session.Email}");
            Log.Information($"Session Items Count: {HttpContext.Items.Count}");

            return Ok(new
            {
                Success = true
            });
        }
    }
}
