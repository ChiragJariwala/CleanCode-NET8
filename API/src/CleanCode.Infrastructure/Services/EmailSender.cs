﻿namespace CleanCode.Infrastructure.Services
{
    // This class is used by the application to send email
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender // : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // TODO: Wire this up to actual email sending logic via SendGrid, local SMTP, etc.
            return Task.CompletedTask;
        }
    }
}
