using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AsyncTest.Controllers
{
    [Route("tasks-example")]
    [ApiController]
    public class TasksExampleController : ControllerBase
    {

        private const int DELAY_TASK = 5000;

        [HttpGet]
        [Route("wrong")]
        public IActionResult GetWrongExample()
        {
            var result = ExampleTask(1).Result;

            return Ok(new
            {
                Result = result
            });
        }

        [HttpGet]
        [Route("right")]
        public async Task<IActionResult> GetRightExample()
        {
            var result = await ExampleTask(1);

            return Ok(new
            {
                Result = result
            });
        }


        [HttpGet]
        [Route("wrong/multiple")]
        public async Task<IActionResult> GetMultipleWrongExample()
        {
            var tasks = new List<Task>()
            {
                ExampleTask(1),
                ExampleTask(2),
                ExampleTask(3),
                ExampleTask(4,true),
            };

            await Task.WhenAll(tasks);

            return Ok();
        }

        [HttpGet]
        [Route("right/multiple")]
        public async Task<IActionResult> GetMultipleRightExample()
        {
            var tasks = new List<Task>()
            {
                ExampleTask(1),
                ExampleTask(2),
                ExampleTask(3),
                ExampleTask(4,true),
            };

            try
            {
                await Task.WhenAll(tasks);
            }
            catch (Exception)
            {
                foreach (var task in tasks)
                {
                    if (task.IsFaulted)
                    {
                        Log.Error($"Handled error: {task.Exception?.InnerException?.Message}");
                    }
                }
            }

            return Ok();
        }



        private async Task<int> ExampleTask(int log, bool throwException = false)
        {
            await Task.Delay(DELAY_TASK);

            Log.Information($"Logging {log}");

            if (throwException)
                throw new Exception("Test exception");

            return log;
        }
    }
}
