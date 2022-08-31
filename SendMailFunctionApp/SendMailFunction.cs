using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SendMailFunctionApp
{
    public class SendMailFunction
    {
        readonly MailService MailService;

        public SendMailFunction(MailService mailService)
        {
            MailService = mailService;
        }

        [FunctionName("SendMailFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# SendMail HTTP trigger function processed a request.");

            string message = req.Query["message"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            message = message ?? data?.message;

            if(string.IsNullOrEmpty(message))
            {
                return new BadRequestObjectResult("Message was not provided");
            }

            try
            {
                await MailService.SendMail("Important message", message);
            }
            catch(Exception ex)
            {
                log.LogError(ex.Message);
                return new BadRequestObjectResult("The email could not be sent.");
            }

            return new OkObjectResult(message);
        }
    }
}
