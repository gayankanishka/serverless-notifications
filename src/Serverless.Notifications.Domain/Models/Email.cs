using System;

namespace Serverless.Notifications.Domain.Models;

public class Email
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? FromAddress { get; set; }

    public string FromName { get; set; }

    public string ToAddress { get; set; } = string.Empty;

    public string ToName { get; set; }

    public string Subject { get; set; } = string.Empty;

    public string EmailTextContent { get; set; } = string.Empty;

    public string EmailHtmlContent { get; set; } = string.Empty;
}