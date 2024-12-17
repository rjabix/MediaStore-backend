using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

namespace UserMService;

sealed class EmailSender(ILogger<EmailSender> logger) : IEmailSender
{
    private readonly ILogger _logger = logger;

    private List<Email> Emails { get; set; } = [];

    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        _logger.LogWarning($"{email} {subject} {htmlMessage}");
        Emails.Add(new Email(email, subject, htmlMessage));
        return Task.CompletedTask;
    }
}

sealed record Email(string Address, string Subject, string HtmlMessage);