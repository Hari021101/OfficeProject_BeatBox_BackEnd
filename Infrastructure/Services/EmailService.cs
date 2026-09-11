using Application.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration config, ILogger<EmailService> logger)
    {
        _config = config;
        _logger = logger;
    }

    public async Task SendEmailAsync(
        string toEmail,
        string subject,
        string htmlBody)
    {
        var smtpHost = _config["Email:SmtpHost"] ?? "smtp.gmail.com";
        var smtpPort = int.TryParse(_config["Email:SmtpPort"], out var port) ? port : 587;

        var senderEmail = _config["Email:SenderEmail"] ?? "";
        var senderName = _config["Email:SenderName"] ?? "BeatBox";

        var appPassword = _config["Email:AppPassword"] ?? "";

        if (string.IsNullOrWhiteSpace(smtpHost) || string.IsNullOrWhiteSpace(senderEmail))
        {
            _logger.LogError("Email service configuration missing: SmtpHost or SenderEmail is empty.");
            throw new InvalidOperationException("Email service configuration is incomplete.");
        }

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

        try
        {
            using var client = new SmtpClient();

            var socketOptions = smtpPort == 465
                ? SecureSocketOptions.SslOnConnect
                : SecureSocketOptions.StartTls;

            await client.ConnectAsync(
                smtpHost,
                smtpPort,
                socketOptions);

            if (!string.IsNullOrWhiteSpace(appPassword))
            {
                await client.AuthenticateAsync(
                    senderEmail,
                    appPassword);
            }

            await client.SendAsync(message);
            await client.DisconnectAsync(true);

            _logger.LogInformation("Successfully sent email to {ToEmail} via SMTP host {SmtpHost}:{SmtpPort}", toEmail, smtpHost, smtpPort);
        }
        catch (SmtpCommandException ex)
        {
            _logger.LogError(ex, "SMTP Server Error ({StatusCode}) sending email to {ToEmail} via {SmtpHost}: {Message}", ex.StatusCode, toEmail, smtpHost, ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email to {ToEmail} via {SmtpHost}", toEmail, smtpHost);
            throw;
        }
    }
}