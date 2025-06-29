using IOptionsDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace IOptionsDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OptionsDemoController(
        StaticOptionsService staticOptionsService,
        SnapshotOptionsService snapshotOptionsService,
        MonitorOptionsService monitorOptionsService) : ControllerBase
    {

        [HttpGet("static")]
        public IActionResult GetStatic()
        {
            var message = staticOptionsService.GetMessage();
            return Ok(new { Source = "IOptions", Message = message });
        }


        [HttpGet("snapshot")]
        public IActionResult GetSnapshot()
        {
            var message = snapshotOptionsService.GetMessage();
            return Ok(new { Source = "IOptionsSnapshot", Message = message });
        }

        [HttpGet("monitor")]
        public IActionResult GetMonitor()
        {
            var message = monitorOptionsService.GetMessage();
            return Ok(new { Source = "IOptionsMonitor", Message = message });
        }


    }
}
