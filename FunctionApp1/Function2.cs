using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;

namespace FunctionApp1
{
    public static class Function5
    {
        [FunctionName("Function5")]
        [return: SendGrid(ApiKey = "ApiKeySendGrid", To = "{EmailPara}", From = "jcguimaraes@gmail.com")]
        public static SendGridMessage Run([QueueTrigger("filamail", Connection = "StorageAccount")] Email email, ILogger log)
        {
            log.LogInformation($"Email assunto: {email.Assunto}");

            SendGridMessage message = new SendGridMessage()
            {
                Subject = email.Assunto
            };

            message.AddContent("text/plain", email.Corpo);
            return message;
        }
    }

    public class Email
    {
        public string Assunto { get; set; }
        public string Corpo { get; set; }
        public string EmailPara { get; set; }
    }
}
