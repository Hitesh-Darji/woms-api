namespace WOMS.Application.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(
            string toEmail,
            string subject,
            string body,
            bool isHtml = false,
            CancellationToken cancellationToken = default);

        Task<bool> SendEmailAsync(
            List<string> toEmails,
            string subject,
            string body,
            bool isHtml = false,
            CancellationToken cancellationToken = default);

        Task<bool> SendEmailWithTemplateAsync(
            List<string> toEmails,
            string subject,
            string template,
            Dictionary<string, object>? templateData = null,
            CancellationToken cancellationToken = default);
    }
}

