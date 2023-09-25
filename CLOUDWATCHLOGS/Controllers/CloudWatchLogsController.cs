using Microsoft.AspNetCore.Mvc; 


namespace CLOUDWATCHLOGS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CloudWatchLogsController : ControllerBase
    {
        private readonly ILogger<CloudWatchLogsController> _logger;

        public CloudWatchLogsController(ILogger<CloudWatchLogsController> logger)
        {
            _logger = logger;
        }

        // GET: api/<CloudWatchLogsController>
        [HttpGet]
        public ActionResult SendLog()
        {
            _logger.LogInformation("This is an information log");
            _logger.LogWarning("This is an warning log");

            string criticalMessage = "This is an critical message";
            _logger.LogCritical("Critical Message: {0}", criticalMessage);

            try
            {
                throw new NotImplementedException();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occured");
            }
            return Ok("completed");
        }
    }
}
