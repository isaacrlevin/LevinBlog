using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Communication.Email.Models;
using System.Collections.Generic;
using Azure;
using Azure.Communication.Email;
using System.Threading;

namespace ContactForm
{
    public static class ContactForm
    {
        [FunctionName("ContactForm")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var formdata = await req.ReadFormAsync();

            var fromName = System.Net.WebUtility.HtmlDecode(formdata["fromName"]);
            var fromEmail = System.Net.WebUtility.HtmlDecode(formdata["fromEmail"]);
            var body = System.Net.WebUtility.HtmlDecode(formdata["body"]);

            // This code demonstrates how to fetch your connection string
            // from an environment variable.
            string connectionString = Environment.GetEnvironmentVariable("ACSConnectionString");
            EmailClient emailClient = new EmailClient(connectionString);

            //Replace with your domain and modify the content, recipient details as required

            EmailContent emailContent = new EmailContent($"Contact form submitted by {fromName} <{fromEmail}>");
            emailContent.PlainText = body;
            emailContent.Html = body;
            List<EmailAddress> emailAddresses = new List<EmailAddress> { new EmailAddress(Environment.GetEnvironmentVariable("ToEmail")) { DisplayName = "Isaac Levin" } };
            EmailRecipients emailRecipients = new EmailRecipients(emailAddresses);
            EmailMessage emailMessage = new EmailMessage("DoNotReply@isaaclevin.com", emailContent, emailRecipients);
            SendEmailResult emailResult = emailClient.Send(emailMessage, CancellationToken.None);

            Console.WriteLine($"MessageId = {emailResult.MessageId}");

            Response<SendStatusResult> messageStatus = null;
            messageStatus = emailClient.GetSendStatus(emailResult.MessageId);
            Console.WriteLine($"MessageStatus = {messageStatus.Value.Status}");
            TimeSpan duration = TimeSpan.FromMinutes(3);
            long start = DateTime.Now.Ticks;
            do
            {
                messageStatus = emailClient.GetSendStatus(emailResult.MessageId);
                if (messageStatus.Value.Status != SendStatus.Queued)
                {
                    Console.WriteLine($"MessageStatus = {messageStatus.Value.Status}");
                    break;
                }
                Thread.Sleep(10000);
                Console.WriteLine($"...");

            } while (DateTime.Now.Ticks - start < duration.Ticks);

            return new OkObjectResult(messageStatus.Value.Status.ToString());
        }
    }
}
