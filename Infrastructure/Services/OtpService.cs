using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace Infrastructure.Services;

public class OtpService : IOtpService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;
    private readonly ILogger<OtpService> _logger;
    private readonly IEmailService _emailService;

    public OtpService(AppDbContext context, IConfiguration config, ILogger<OtpService> logger, IEmailService emailService)
    {
        _context = context;
        _config = config;
        _logger = logger;
        _emailService = emailService;
    }

    // ─── Generate a 6-digit OTP and store it ─────────────────────────────────
    private async Task<string> CreateOtpAsync(string userId, OtpType type)
    {
        // Invalidate any previous unused OTPs of same type for this user
        var existing = await _context.OtpRecords
            .Where(o => o.UserId == userId && o.Type == type && !o.IsUsed)
            .ToListAsync();
        existing.ForEach(o => o.IsUsed = true);

        var code = new Random().Next(100000, 999999).ToString();
        var expiry = int.Parse(_config["Otp:ExpiryMinutes"] ?? "10");

        _context.OtpRecords.Add(new OtpRecord
        {
            UserId = userId,
            Code = code,
            Type = type,
            ExpiresAt = DateTime.UtcNow.AddMinutes(expiry),
            IsUsed = false
        });

        await _context.SaveChangesAsync();
        return code;
    }

    // ─── Email OTP via Gmail SMTP ─────────────────────────────────────────────
    public async Task SendEmailOtpAsync(string userId, string email)
    {
        var code = await CreateOtpAsync(userId, OtpType.Email);
        var devMode = bool.Parse(_config["Otp:DevMode"] ?? "true");

        if (devMode)
        {
            // In dev mode: just log to console — no real email sent
            _logger.LogWarning("╔══════════════════════════════════════╗");
            _logger.LogWarning("║  📧 EMAIL OTP for {Email}", email);
            _logger.LogWarning("║  Code: {Code}  (valid 10 minutes)", code);
            _logger.LogWarning("╚══════════════════════════════════════╝");
            return;
        }

        // Production: send via Gmail SMTP using MailKit
        var smtpHost = _config["Email:SmtpHost"] ?? "smtp.gmail.com";
        var smtpPort = int.Parse(_config["Email:SmtpPort"] ?? "587");
        var senderEmail = _config["Email:SenderEmail"] ?? "";
        var senderName = _config["Email:SenderName"] ?? "BeatBox";
        var appPassword = _config["Email:AppPassword"] ?? "";
        var htmlTemplate = $"""
<div style="font-family:Arial,sans-serif">
<h2>BeatBox Verification</h2>
<p>Your OTP is:</p>
<h1>{code}</h1>
<p>Expires in 10 minutes.</p>
</div>
""";        

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(senderName, senderEmail));
        message.To.Add(MailboxAddress.Parse(email));
        message.Subject = $"BeatBox — Your Verification Code: {code}";
        message.Body = new TextPart("html")
        {
            Text = $"""
                <div style="font-family:Arial,sans-serif;background:#060b19;color:#fff;padding:40px;border-radius:16px;max-width:480px;margin:auto">
                  <h2 style="color:#00f3ff;margin-bottom:8px">🎧 BeatBox</h2>
                  <h3 style="margin-bottom:24px">Email Verification</h3>
                  <p style="color:#aaa">Use the code below to verify your email address. It expires in 10 minutes.</p>
                  <div style="background:#0d1117;border:2px solid #00f3ff;border-radius:12px;padding:24px;text-align:center;margin:24px 0">
                    <span style="font-size:40px;font-weight:900;letter-spacing:12px;color:#00f3ff">{code}</span>
                  </div>
                  <p style="color:#555;font-size:12px">If you did not request this, please ignore this email.</p>
                </div>
                """
        };

        await _emailService.SendEmailAsync(
        email,
        $"BeatBox — Your Verification Code: {code}",
        htmlTemplate);
    }

    // ─── Phone OTP — console log (free), extend with SMS provider later ───────
    public async Task SendPhoneOtpAsync(string userId, string phoneNumber)
    {
        var code = await CreateOtpAsync(userId, OtpType.Phone);

        // Always log to console — replace this block with Twilio/Fast2SMS later
        _logger.LogWarning("╔══════════════════════════════════════╗");
        _logger.LogWarning("║  📱 PHONE OTP for {Phone}", phoneNumber);
        _logger.LogWarning("║  Code: {Code}  (valid 10 minutes)", code);
        _logger.LogWarning("╚══════════════════════════════════════╝");

        await Task.CompletedTask;
    }

    // ─── Verify OTP ───────────────────────────────────────────────────────────
    public async Task<bool> VerifyOtpAsync(string userId, string code, OtpType type)
    {
        var otp = await _context.OtpRecords
            .Where(o =>
                o.UserId == userId &&
                o.Code == code &&
                o.Type == type &&
                !o.IsUsed &&
                o.ExpiresAt > DateTime.UtcNow)
            .OrderByDescending(o => o.ExpiresAt)
            .FirstOrDefaultAsync();

        if (otp == null) return false;

        otp.IsUsed = true;
        await _context.SaveChangesAsync();
        return true;
    }
}
