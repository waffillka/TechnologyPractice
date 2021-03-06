using Contracts;
using Entities.EmailServiceModels;
using MailKit.Net.Smtp;
using MimeKit;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmailService
{
    public class EmailService : IEmailService
    {
        public EmailService()
        {

        }

        public async Task SendAsync(MessageContext message)
        {
            EmailSettings emailSettings;
            //using (FileStream fs = new FileStream(string.Concat(Directory.GetCurrentDirectory(), "/nEmailSettings.json"), FileMode.Open))
            using (FileStream fs = new FileStream("d:\\EPAM\\InnowiseGroup\\TechnologyPractice\\TechnologyPractice\\MailService\\EmailSettings.json", FileMode.Open))
            {
                emailSettings = await JsonSerializer.DeserializeAsync<EmailSettings>(fs);
            }

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(emailSettings.Name, emailSettings.Email));
            emailMessage.To.Add(new MailboxAddress(message.FullName, message.EmailTo));
            emailMessage.Subject = message.Topic;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message.Message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync($"smtp.{emailSettings.Email.Split('@')[1]}", 465, true);
                await client.AuthenticateAsync(emailSettings.Email, emailSettings.Password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }

        public async Task SendAsync(string email, string name, string topic, string message)
        {
            var messageContext = new MessageContext(email, name, topic, message);
            await SendAsync(messageContext);
        }
    }
}
