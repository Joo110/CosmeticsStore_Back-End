// EmailService.cs
using CosmeticsStore.Domain.Interfaces.Services;
using CosmeticsStore.Domain.Models;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CosmeticsStore.Infrastructure.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfig _emailConfig;

        public EmailService(IOptions<EmailConfig> emailConfig)
        {
            _emailConfig = emailConfig.Value;
        }

        public async Task SendAsync(EmailRequest emailRequest, CancellationToken cancellationToken = default)
        {
            var emailMessage = CreateEmail(emailRequest);

            using var smtpClient = new SmtpClient();

            try
            {
                // Connect to SMTP server
                await smtpClient.ConnectAsync(
                    _emailConfig.Server,
                    _emailConfig.Port,
                    _emailConfig.UseSsl,
                    cancellationToken);

                // Authenticate
                await smtpClient.AuthenticateAsync(
                    _emailConfig.Username,
                    _emailConfig.Password,
                    cancellationToken);

                // Send email
                await smtpClient.SendAsync(emailMessage, cancellationToken);
            }
            finally
            {
                await smtpClient.DisconnectAsync(quit: true, cancellationToken);
            }
        }

        private MimeMessage CreateEmail(EmailRequest emailRequest)
        {
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(_emailConfig.FromEmail));
            email.To.AddRange(emailRequest.ToEmails.Select(MailboxAddress.Parse));
            email.Subject = emailRequest.Subject;

            var bodyBuilder = new BodyBuilder
            {
                TextBody = emailRequest.Body
            };

            // Attachments
            foreach (var attachment in emailRequest.Attachments)
            {
                bodyBuilder.Attachments.Add(
                    attachment.FileName,
                    new ContentType(attachment.MediaType, attachment.SubMediaType));
            }

            email.Body = bodyBuilder.ToMessageBody();

            return email;
        }
    }
}
