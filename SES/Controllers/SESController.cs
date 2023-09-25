using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SES.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SESController : ControllerBase
    {
        private readonly IAmazonSimpleEmailService _amazonSimpleEmailService;
        private readonly string _fromAddress = "manikandan@whiteblue.in"; /* Need to verify email address from Amazon SES */
        private readonly string _toAddress = "manikandan@whiteblue.in"; /* Need to verify email address from Amazon SES */
        private readonly string _subject = "SES";
        private readonly string _body = "<h1>Testing Amazon SES service</h1><p>It worked</p>";

        public SESController(IAmazonSimpleEmailService amazonSimpleEmailService)
        {
            _amazonSimpleEmailService = amazonSimpleEmailService;
        }

        [HttpGet]
        public async Task<IActionResult> SendMail()
        {
            SendEmailRequest sendEmailRequest = new SendEmailRequest()
            {
                Destination = new Destination() { ToAddresses = new List<string>() { _toAddress } },
                Message = new Message()
                {
                    Body = new Body()
                    {
                        Html = new Content()
                        {
                            Charset = "UTF-8",
                            Data = _body
                        }
                    },
                    Subject = new Content()
                    {
                        Charset = "UTF-8",
                        Data = _subject
                    }
                },
                Source = _fromAddress
            };

            try
            {
                var sendEmailResult = await _amazonSimpleEmailService.SendEmailAsync(sendEmailRequest);
                if (sendEmailResult.HttpStatusCode != System.Net.HttpStatusCode.OK)
                {
                    return BadRequest("Something went wrong!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error occured. Message: " + ex.Message);
            }
            
            return Ok("Email sent");
        }
    }
}
