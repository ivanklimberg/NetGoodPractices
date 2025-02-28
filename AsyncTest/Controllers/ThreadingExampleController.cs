using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Concurrent;

namespace AsyncTest.Controllers
{
    [Route("threading-example")]
    [ApiController]
    public class ThreadingExampleController : Controller
    {
        [HttpGet]
        [Route("error")]
        public async Task<IActionResult> Error()
        {
            var unsafeList = new List<int>();
            int itemCount = 10000;

            try
            {
                Parallel.ForEach(Enumerable.Range(1, itemCount), i =>
                {
                    unsafeList.Add(i); // May cause race condition
                });

                Log.Information($"Non-thread-safe List Count: {unsafeList.Count}");
            }
            catch (Exception ex)
            {
                Log.Error("Exception in List");
            }

            return Ok(new
            {
                ListCount = unsafeList.Count
            });
        }

        [HttpGet]
        [Route("correct")]
        public async Task<IActionResult> Correct()
        {
            var safeList = new ConcurrentBag<int>();
            int itemCount = 10000;

            Parallel.ForEach(Enumerable.Range(1, itemCount), i =>
            {
                safeList.Add(i);
            });

            Log.Information($"Thread-safe List Count: {safeList.Count}");

            return Ok(new
            {
                ListCount = safeList.Count
            });
        }


    }
}
