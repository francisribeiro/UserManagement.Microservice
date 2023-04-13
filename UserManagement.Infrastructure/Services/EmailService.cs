using UserManagement.Infrastructure.Contracts;

namespace UserManagement.Infrastructure.Services;

public class EmailService : IEmailService
{
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        // Implement the actual email sending logic here using SMTP, a third-party email service, or any other method
        await Task.CompletedTask;
    }
}