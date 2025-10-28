using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WOMS.Application.Services;

namespace WOMS.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;
        private readonly string _fromEmail;
        private readonly string _fromName;
        private readonly bool _useSsl;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;

            var emailConfig = _configuration.GetSection("Email");
            _smtpServer = emailConfig.GetValue<string>("SmtpServer") ?? "smtp.gmail.com";
            _smtpPort = emailConfig.GetValue<int>("SmtpPort");
            _smtpUsername = emailConfig.GetValue<string>("SmtpUsername") ?? string.Empty;
            _smtpPassword = emailConfig.GetValue<string>("SmtpPassword") ?? string.Empty;
            _fromEmail = emailConfig.GetValue<string>("FromEmail") ?? string.Empty;
            _fromName = emailConfig.GetValue<string>("FromName") ?? "WOMS System";
            _useSsl = emailConfig.GetValue<bool>("UseSsl");
        }

        public async Task<bool> SendEmailAsync(
            string toEmail,
            string subject,
            string body,
            bool isHtml = false,
            CancellationToken cancellationToken = default)
        {
            return await SendEmailAsync(new List<string> { toEmail }, subject, body, isHtml, cancellationToken);
        }

        public async Task<bool> SendEmailAsync(
            List<string> toEmails,
            string subject,
            string body,
            bool isHtml = false,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrEmpty(_smtpServer) || string.IsNullOrEmpty(_smtpUsername))
                {
                    _logger.LogWarning("Email configuration is missing. Cannot send email.");
                    return false;
                }

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_fromName, _fromEmail));

                foreach (var email in toEmails)
                {
                    message.To.Add(new MailboxAddress("", email));
                }

                message.Subject = subject;

                var bodyBuilder = new BodyBuilder();
                if (isHtml)
                {
                    bodyBuilder.HtmlBody = body;
                }
                else
                {
                    bodyBuilder.TextBody = body;
                }

                message.Body = bodyBuilder.ToMessageBody();

                using var client = new SmtpClient();
                await client.ConnectAsync(_smtpServer, _smtpPort, _useSsl, cancellationToken);

                if (!string.IsNullOrEmpty(_smtpUsername))
                {
                    await client.AuthenticateAsync(_smtpUsername, _smtpPassword, cancellationToken);
                }

                await client.SendAsync(message, cancellationToken);
                await client.DisconnectAsync(true, cancellationToken);

                _logger.LogInformation("Email sent successfully to {Recipients}", string.Join(", ", toEmails));
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {Recipients}", string.Join(", ", toEmails));
                return false;
            }
        }

        public async Task<bool> SendEmailWithTemplateAsync(
            List<string> toEmails,
            string subject,
            string template,
            Dictionary<string, object>? templateData = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                // Simple template replacement
                var body = template;

                if (templateData != null)
                {
                    foreach (var (key, value) in templateData)
                    {
                        body = body.Replace($"{{{key}}}", value?.ToString() ?? string.Empty);
                    }
                }

                return await SendEmailAsync(toEmails, subject, body, isHtml: true, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email with template to {Recipients}", string.Join(", ", toEmails));
                return false;
            }
        }
    }
}

