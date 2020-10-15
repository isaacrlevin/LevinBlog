using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
namespace LevinBlog
{
    public static class ContactForm
    {
        [FunctionName("ContactForm")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var formdata = await req.ReadFormAsync();

            var fromName = System.Net.WebUtility.HtmlDecode(formdata["fromName"]);
            var fromEmail = System.Net.WebUtility.HtmlDecode(formdata["fromEmail"]);
            var body = System.Net.WebUtility.HtmlDecode(formdata["body"]);

            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(fromEmail, fromName);
            var subject = $"Contact form submitted by {fromName} <{fromEmail}>";
            var to = new EmailAddress(Environment.GetEnvironmentVariable("ToEmail"));
            var plainTextContent = body;
            var htmlContent = body;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);

            return new OkObjectResult("Success");
        }
    }
}
