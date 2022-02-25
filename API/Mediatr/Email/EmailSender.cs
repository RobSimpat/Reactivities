using MailKit.Net.Smtp;
using MimeKit;

namespace API.Mediatr.Email
{
    public class EmailSender : IEmailSender
    {

        public EmailSender(EmailConfiguration emailConfiguration)
        {
            EmailConfiguration = emailConfiguration;
        }

        public EmailConfiguration EmailConfiguration { get; }

        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage= new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(EmailConfiguration.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject=message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using(var client=new SmtpClient())
            {
                try
                {
                    client.Connect(EmailConfiguration.SmtpServer, EmailConfiguration.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(EmailConfiguration.UserName, EmailConfiguration.Password);
                    client.Send(mailMessage);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

    }
}
