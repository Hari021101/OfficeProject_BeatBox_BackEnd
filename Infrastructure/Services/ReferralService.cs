using Application.Common.Options;
using Application.DTOs.Referral;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services;

public class ReferralService : IReferralService
{
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly FrontendOptions _frontendOptions;
    private readonly ReferralOptions _referralOptions;
    private readonly ILogger<ReferralService> _logger;

    public ReferralService(
        AppDbContext context,
        UserManager<AppUser> userManager,
        IOptions<FrontendOptions> frontendOptions,
        IOptions<ReferralOptions> referralOptions,
        ILogger<ReferralService> logger)
    {
        _context = context;
        _userManager = userManager;
        _frontendOptions = frontendOptions.Value;
        _referralOptions = referralOptions.Value;
        _logger = logger;
    }

    public async Task<string> GetUserReferralCodeAsync(string userId, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        if (user == null) return string.Empty;

        if (!string.IsNullOrWhiteSpace(user.ReferralCode))
        {
            return user.ReferralCode;
        }

        // Generate unique code with bounded retries under concurrency
        const int maxAttempts = 10;
        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            var candidateCode = GenerateSecureCode();
            bool exists = await _context.Users.AnyAsync(u => u.ReferralCode == candidateCode, cancellationToken);
            if (!exists)
            {
                user.ReferralCode = candidateCode;
                try
                {
                    await _context.SaveChangesAsync(cancellationToken);
                    return candidateCode;
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogWarning(ex, "Concurrency collision when saving referral code {Code} for user {UserId}. Retrying...", candidateCode, userId);
                    _context.Entry(user).Property(u => u.ReferralCode).IsModified = false;
                }
            }
        }

        throw new InvalidOperationException("Failed to generate a unique referral code after multiple attempts.");
    }

    public async Task<ReferralDashboardDto> GetReferralDashboardAsync(string userId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(_frontendOptions.BaseUrl))
        {
            throw new InvalidOperationException("Frontend:BaseUrl is not configured in application configuration.");
        }

        var userCode = await GetUserReferralCodeAsync(userId, cancellationToken);
        
        var baseUrl = _frontendOptions.BaseUrl.TrimEnd('/');
        var referralLink = $"{baseUrl}/#/ref/{userCode}";

        var referrals = await _context.Referrals
            .AsNoTracking()
            .Where(r => r.ReferrerId == userId)
            .OrderByDescending(r => r.CreatedDate)
            .Select(r => new
            {
                r.Id,
                r.Status,
                r.RewardAmount,
                r.CreatedDate,
                ReferredUserFullName = r.ReferredUser != null ? r.ReferredUser.FullName : null,
                r.ReferredUserEmail
            })
            .ToListAsync(cancellationToken);

        int friendsInvited = referrals.Count;
        int successfulReferrals = referrals.Count(r => r.Status == ReferralStatus.Qualified || r.Status == ReferralStatus.RewardCredited);
        
        // Total Rewards Earned only counts actual credited rewards
        decimal totalRewardsEarned = referrals
            .Where(r => r.Status == ReferralStatus.RewardCredited)
            .Sum(r => r.RewardAmount);

        var history = referrals.Select(r => new ReferralHistoryItemDto
        {
            Id = r.Id,
            FriendName = !string.IsNullOrWhiteSpace(r.ReferredUserFullName)
                ? MaskName(r.ReferredUserFullName)
                : (!string.IsNullOrWhiteSpace(r.ReferredUserEmail) ? MaskEmail(r.ReferredUserEmail) : "BeatBox Member"),
            Status = r.Status.ToString(),
            RewardAmount = r.RewardAmount,
            CreatedDate = r.CreatedDate
        }).ToList();

        return new ReferralDashboardDto
        {
            ReferralCode = userCode,
            ReferralLink = referralLink,
            FriendsInvited = friendsInvited,
            SuccessfulReferrals = successfulReferrals,
            TotalRewardsEarned = totalRewardsEarned,
            History = history
        };
    }

    public async Task<ReferralValidationResultDto> ValidateReferralCodeAsync(string code, string? currentUserId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return new ReferralValidationResultDto { IsValid = false, Message = "Referral code is required." };
        }

        var cleanCode = code.Trim().ToUpperInvariant();
        var referrer = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.ReferralCode == cleanCode, cancellationToken);

        if (referrer == null || !referrer.IsActive)
        {
            return new ReferralValidationResultDto { IsValid = false, Message = "Invalid or inactive referral code." };
        }

        if (!string.IsNullOrEmpty(currentUserId) && referrer.Id == currentUserId)
        {
            return new ReferralValidationResultDto
            {
                IsValid = false,
                Code = cleanCode,
                ReferrerName = referrer.FullName,
                Message = "Self-referrals are not permitted."
            };
        }

        if (!string.IsNullOrEmpty(currentUserId))
        {
            bool alreadyAttributed = await _context.Referrals
                .AsNoTracking()
                .AnyAsync(r => r.ReferredUserId == currentUserId, cancellationToken);

            if (alreadyAttributed)
            {
                return new ReferralValidationResultDto
                {
                    IsValid = false,
                    Code = cleanCode,
                    Message = "You have already claimed a referral code."
                };
            }
        }

        return new ReferralValidationResultDto
        {
            IsValid = true,
            Code = cleanCode,
            ReferrerName = MaskName(referrer.FullName ?? "BeatBox Member"),
            Message = $"Valid referral code! You will receive ₹{_referralOptions.DefaultRewardAmount:N0} off your first purchase."
        };
    }

    public async Task<ApplyReferralResultDto> ApplyReferralAsync(string code, string referredUserId, CancellationToken cancellationToken = default)
    {
        var validation = await ValidateReferralCodeAsync(code, referredUserId, cancellationToken);
        if (!validation.IsValid)
        {
            return new ApplyReferralResultDto { Success = false, Message = validation.Message };
        }

        var cleanCode = code.Trim().ToUpperInvariant();
        var referrer = await _context.Users.FirstOrDefaultAsync(u => u.ReferralCode == cleanCode, cancellationToken);
        if (referrer == null)
        {
            return new ApplyReferralResultDto { Success = false, Message = "Referrer user not found." };
        }

        var referredUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == referredUserId, cancellationToken);
        if (referredUser == null)
        {
            return new ApplyReferralResultDto { Success = false, Message = "Referred user account not found." };
        }

        // Atomic double-claim check
        var existing = await _context.Referrals
            .FirstOrDefaultAsync(r => r.ReferredUserId == referredUserId, cancellationToken);

        if (existing != null)
        {
            return new ApplyReferralResultDto { Success = false, Message = "A referral code has already been linked to this account." };
        }

        var referral = new Referral
        {
            ReferrerId = referrer.Id,
            ReferredUserId = referredUser.Id,
            ReferredUserEmail = referredUser.Email ?? string.Empty,
            ReferralCode = cleanCode,
            Status = ReferralStatus.Pending,
            RewardAmount = _referralOptions.DefaultRewardAmount,
            CreatedDate = DateTime.UtcNow
        };

        _context.Referrals.Add(referral);

        try
        {
            await _context.SaveChangesAsync(cancellationToken);
            return new ApplyReferralResultDto
            {
                Success = true,
                Message = "Referral code applied successfully!",
                ReferralId = referral.Id
            };
        }
        catch (DbUpdateException ex)
        {
            _logger.LogWarning(ex, "Duplicate referral attribution attempt for user {UserId}", referredUserId);
            return new ApplyReferralResultDto
            {
                Success = false,
                Message = "Referral code has already been applied."
            };
        }
    }

    public async Task ProcessQualifyingOrderAsync(int orderId, string userId, decimal orderTotal, CancellationToken cancellationToken = default)
    {
        if (orderTotal < _referralOptions.MinimumQualifyingOrderTotal)
        {
            _logger.LogInformation("Order {OrderId} total {Total} is below minimum qualifying referral threshold {Threshold}", orderId, orderTotal, _referralOptions.MinimumQualifyingOrderTotal);
            return;
        }

        // Find pending referral for this user
        var referral = await _context.Referrals
            .Include(r => r.Referrer)
            .FirstOrDefaultAsync(r => r.ReferredUserId == userId && r.Status == ReferralStatus.Pending, cancellationToken);

        if (referral == null) return;

        // Verify self-referral protection
        if (referral.ReferrerId == userId)
        {
            referral.Status = ReferralStatus.Expired;
            await _context.SaveChangesAsync(cancellationToken);
            return;
        }

        // Verify order has not already rewarded
        bool orderAlreadyRewarded = await _context.Referrals
            .AnyAsync(r => r.QualifyingOrderId == orderId, cancellationToken);

        if (orderAlreadyRewarded)
        {
            _logger.LogWarning("Order {OrderId} has already been processed for a referral reward.", orderId);
            return;
        }

        // Idempotent state transition
        referral.Status = ReferralStatus.RewardCredited;
        referral.QualifyingOrderId = orderId;
        referral.QualifiedDate = DateTime.UtcNow;

        if (referral.Referrer != null)
        {
            referral.Referrer.RewardBalance += referral.RewardAmount;

            // Audit Ledger Entry
            var rewardTx = new RewardTransaction
            {
                UserId = referral.ReferrerId,
                ReferralId = referral.Id,
                OrderId = orderId,
                Amount = referral.RewardAmount,
                TransactionType = "ReferralCredit",
                Description = $"Referral reward for qualifying first order #{orderId}",
                CreatedDate = DateTime.UtcNow
            };
            _context.RewardTransactions.Add(rewardTx);
        }

        await _context.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Successfully credited referral reward ₹{Reward} to user {ReferrerId} for order #{OrderId}", referral.RewardAmount, referral.ReferrerId, orderId);
    }

    private static string GenerateSecureCode()
    {
        const string chars = "23456789ABCDEFGHJKLMNPQRSTUVWXYZ"; // Exclude easily confused chars (0,1,O,I)
        var bytes = new byte[6];
        RandomNumberGenerator.Fill(bytes);
        var sb = new StringBuilder("BB");
        foreach (byte b in bytes)
        {
            sb.Append(chars[b % chars.Length]);
        }
        return sb.ToString();
    }

    private static string MaskName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return "BeatBox Member";
        var parts = name.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 1)
        {
            return parts[0].Length > 2 ? $"{parts[0][0]}***" : parts[0];
        }
        return $"{parts[0]} {parts[^1][0]}.";
    }

    private static string MaskEmail(string email)
    {
        var parts = email.Split('@');
        if (parts.Length < 2) return "BeatBox Member";
        var name = parts[0];
        if (name.Length <= 2) return $"{name}***@{parts[1]}";
        return $"{name[..2]}***@{parts[1]}";
    }
}
