using Application.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendEmailAsync(
        string toEmail,
        string subject,
        string htmlBody)
    {
        var smtpHost = _config["Email:SmtpHost"];
        var smtpPort = int.Parse(_config["Email:SmtpPort"]!);

        var senderEmail = _config["Email:SenderEmail"];
        var senderName = _config["Email:SenderName"];

        var appPassword = _config["Email:AppPassword"];

        var message = new MimeMessage();

        message.From.Add(
            new MailboxAddress(
                senderName,
                senderEmail));

        message.To.Add(
            MailboxAddress.Parse(toEmail));

        message.Subject = subject;

        message.Body = new TextPart("html")
        {
            Text = htmlBody
        };

        using var client = new SmtpClient();

        await client.ConnectAsync(
            smtpHost,
            smtpPort,
            SecureSocketOptions.StartTls);

        await client.AuthenticateAsync(
            senderEmail,
            appPassword);

        await client.SendAsync(message);

        await client.DisconnectAsync(true);
    }
}