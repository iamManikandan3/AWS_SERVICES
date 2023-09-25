using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SNS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SNSController : ControllerBase
    {
        private readonly IAmazonSimpleNotificationService _amazonNotificationService;

        public SNSController(IAmazonSimpleNotificationService amazonNotificationService)
        {
            _amazonNotificationService = amazonNotificationService;
        }

        [HttpGet]
        public async Task<ActionResult> SendNotification()
        {
            var request = new PublishRequest()
            {
                Message = $"Test at {DateTime.Now.ToLongDateString()}",
                TopicArn = "arn:aws:sns:us-east-1:612953658344:DotnetTestTopic"
            };

            var response = await _amazonNotificationService.PublishAsync(request);

            return Ok("Message Sent");
        }
    }
}
