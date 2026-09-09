using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly IOtpService _otpService;
        private readonly IAuditLogService _auditLogService;
        private readonly AppDbContext _context;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ITokenService tokenService,
            IOtpService otpService,
            IAuditLogService auditLogService,
            AppDbContext context,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _otpService = otpService;
            _auditLogService = auditLogService;
            _context = context;
            _logger = logger;
        }

        // ─── POST /api/account/register ───────────────────────────────────────
        [HttpPost("register")]
        public async Task<ActionResult<RegisterResponseDto>> Register([FromBody] RegisterDto registerDto)
        {
            var isEmail = registerDto.Identifier.Contains('@');

            if (isEmail && await _userManager.FindByEmailAsync(registerDto.Identifier) != null)
                return BadRequest("Email is already registered.");

            if (!isEmail && _userManager.Users.Any(u => u.PhoneNumber == registerDto.Identifier))
                return BadRequest("Phone number is already registered.");

            var user = new AppUser
            {
                FullName = registerDto.FullName,
                UserName = registerDto.Identifier,
                Email = isEmail ? registerDto.Identifier : null,
                PhoneNumber = !isEmail ? registerDto.Identifier : null,
                IsEmailVerified = false,
                IsPhoneVerified = false,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(error.Code, error.Description);
                return BadRequest(ModelState);
            }

            if (isEmail)
                await _otpService.SendEmailOtpAsync(user.Id, user.Email!);
            else
                await _otpService.SendPhoneOtpAsync(user.Id, user.PhoneNumber!);

            return Ok(new RegisterResponseDto
            {
                UserId = user.Id,
                Identifier = registerDto.Identifier,
                IdentifierType = isEmail ? "email" : "phone",
                Message = $"Account created. Please check your {(isEmail ? "email" : "phone")} for the verification code."
            });
        }

        // ─── POST /api/account/login ──────────────────────────────────────────
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto loginDto)
        {
            AppUser? user = null;

            if (loginDto.Identifier.Contains('@'))
                user = await _userManager.FindByEmailAsync(loginDto.Identifier);
            else
                user = _userManager.Users.FirstOrDefault(u => u.PhoneNumber == loginDto.Identifier);

            if (user == null) return Unauthorized("Invalid credentials.");

            // Block suspended accounts from logging in
            if (!user.IsActive)
                return Unauthorized("This account has been suspended. Please contact support.");

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result) return Unauthorized("Invalid credentials.");

            var roles = await _userManager.GetRolesAsync(user);

            return new AuthResponseDto
            {
                FullName = user.FullName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                Token = await _tokenService.CreateToken(user),
                Roles = roles
            };
        }

        // ─── GET /api/account/users ───────────────────────────────────────────
        [Authorize(Roles = "Admin")]
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = _userManager.Users.OrderByDescending(u => u.CreatedDate).ToList();
            var result = new List<UserListDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                result.Add(new UserListDto
                {
                    Id = user.Id,
                    FullName = user.FullName ?? string.Empty,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Roles = roles,
                    IsActive = user.IsActive,
                    JoinDate = user.CreatedDate
                });
            }

            return Ok(result);
        }

        // ─── PUT /api/account/{id}/toggle-status ──────────────────────────────
        /// <summary>Suspend or reactivate a user account. Admin only.</summary>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/toggle-status")]
        public async Task<IActionResult> ToggleStatus(string id)
        {
            var target = await _userManager.FindByIdAsync(id);
            if (target == null) return NotFound("User not found.");

            // Prevent admin from suspending themselves
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (adminId == id)
                return BadRequest("You cannot suspend your own account.");

            target.IsActive = !target.IsActive;
            var updateResult = await _userManager.UpdateAsync(target);

            if (!updateResult.Succeeded)
                return StatusCode(500, "Failed to update account status.");

            // Audit log
            var adminName = User.FindFirstValue(ClaimTypes.GivenName)
                            ?? User.FindFirstValue(ClaimTypes.Name)
                            ?? "Admin";

            var action = target.IsActive ? "ACTIVATED" : "SUSPENDED";
            await _auditLogService.LogActionAsync(
                adminId: adminId ?? string.Empty,
                adminName: adminName,
                action: action,
                target: $"User: {target.FullName ?? target.Email ?? id}",
                details: $"Account {(target.IsActive ? "reactivated" : "suspended")} by admin.",
                icon: target.IsActive ? "UserCheck" : "UserX",
                colorClass: target.IsActive ? "text-success" : "text-danger",
                bgClass: target.IsActive ? "bg-success" : "bg-danger"
            );

            return Ok(new ToggleStatusResponseDto
            {
                UserId = target.Id,
                IsActive = target.IsActive,
                Message = $"{target.FullName ?? target.Email} has been {(target.IsActive ? "reactivated" : "suspended")}."
            });
        }

        // ─── PUT /api/account/{id}/toggle-role ───────────────────────────────
        /// <summary>Promote to Admin or demote to Customer. Admin only.</summary>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/toggle-role")]
        public async Task<IActionResult> ToggleRole(string id)
        {
            var target = await _userManager.FindByIdAsync(id);
            if (target == null) return NotFound("User not found.");

            // Prevent self-demotion
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (adminId == id)
                return BadRequest("You cannot change your own role.");

            var currentRoles = await _userManager.GetRolesAsync(target);
            bool isCurrentlyAdmin = currentRoles.Contains("Admin");

            string newRole;
            string removedRole;

            if (isCurrentlyAdmin)
            {
                removedRole = "Admin";
                newRole = "Customer";
            }
            else
            {
                removedRole = "Customer";
                newRole = "Admin";

                // Ensure Admin role exists
                if (!await _roleManager.RoleExistsAsync("Admin"))
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // Ensure Customer role exists
            if (!await _roleManager.RoleExistsAsync("Customer"))
                await _roleManager.CreateAsync(new IdentityRole("Customer"));

            // Swap roles
            if (currentRoles.Contains(removedRole))
                await _userManager.RemoveFromRoleAsync(target, removedRole);

            await _userManager.AddToRoleAsync(target, newRole);

            // Audit log
            var adminName = User.FindFirstValue(ClaimTypes.GivenName)
                            ?? User.FindFirstValue(ClaimTypes.Name)
                            ?? "Admin";

            await _auditLogService.LogActionAsync(
                adminId: adminId ?? string.Empty,
                adminName: adminName,
                action: isCurrentlyAdmin ? "DEMOTED" : "PROMOTED",
                target: $"User: {target.FullName ?? target.Email ?? id}",
                details: $"Role changed from {removedRole} to {newRole} by admin.",
                icon: isCurrentlyAdmin ? "ArrowDownCircle" : "Crown",
                colorClass: isCurrentlyAdmin ? "text-warning" : "text-purple",
                bgClass: isCurrentlyAdmin ? "bg-warning" : "bg-purple"
            );

            return Ok(new ToggleRoleResponseDto
            {
                UserId = target.Id,
                NewRole = newRole,
                Message = $"{target.FullName ?? target.Email} is now a {newRole}."
            });
        }

        // ─── DELETE /api/account ──────────────────────────────────────────────
        /// <summary>Delete currently authenticated customer account. Customer self-delete.</summary>
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteSelfAccount()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User identity could not be verified.");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound("Account not found.");

            var userLabel = user.FullName ?? user.Email ?? userId;
            var success = await DeleteUserAccountInternalAsync(user);
            if (!success)
                return StatusCode(500, "Failed to delete account due to a database constraint or server error.");

            // Audit log
            await _auditLogService.LogActionAsync(
                adminId: userId,
                adminName: userLabel,
                action: "ACCOUNT_DELETED",
                target: $"Self-Delete User: {userLabel}",
                details: "Customer requested permanent account deletion.",
                icon: "UserMinus",
                colorClass: "text-danger",
                bgClass: "bg-danger"
            );

            return Ok(new { success = true, message = "Your account has been deleted successfully." });
        }

        // ─── DELETE /api/account/users/{id} ─────────────────────────────────
        /// <summary>Delete customer account. Admin only.</summary>
        [Authorize(Roles = "Admin")]
        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUserByAdmin(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest("User ID is required.");

            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (adminId == id)
                return BadRequest("You cannot delete your own account through the admin account management endpoint. Please use the self-delete option in Settings.");

            var target = await _userManager.FindByIdAsync(id);
            if (target == null)
                return NotFound("User not found.");

            var targetLabel = target.FullName ?? target.Email ?? id;
            var success = await DeleteUserAccountInternalAsync(target);
            if (!success)
                return StatusCode(500, "Failed to delete customer account due to a database error.");

            // Audit log
            var adminName = User.FindFirstValue(ClaimTypes.GivenName)
                            ?? User.FindFirstValue(ClaimTypes.Name)
                            ?? "Admin";

            await _auditLogService.LogActionAsync(
                adminId: adminId ?? string.Empty,
                adminName: adminName,
                action: "ADMIN_DELETED_USER",
                target: $"User: {targetLabel}",
                details: $"Customer account {targetLabel} was permanently deleted by admin.",
                icon: "UserX",
                colorClass: "text-danger",
                bgClass: "bg-danger"
            );

            return Ok(new { success = true, message = $"Account for {targetLabel} deleted successfully." });
        }

        private async Task<bool> DeleteUserAccountInternalAsync(AppUser user)
        {
            var userId = user.Id;
            _logger.LogInformation("Starting account deletion process for User ID: {UserId}, Email: {Email}", userId, user.Email);

            var existingTransaction = _context.Database.CurrentTransaction;
            Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction? createdTransaction = null;

            if (existingTransaction == null)
            {
                createdTransaction = await _context.Database.BeginTransactionAsync();
            }

            try
            {
                // 1. Remove Carts & CartItems
                var carts = await _context.Carts.Where(c => c.UserId == userId).ToListAsync();
                if (carts.Any())
                {
                    var cartIds = carts.Select(c => c.CartId).ToList();
                    var cartItems = await _context.CartItems.Where(ci => cartIds.Contains(ci.CartId)).ToListAsync();
                    _context.CartItems.RemoveRange(cartItems);
                    _context.Carts.RemoveRange(carts);
                    _logger.LogInformation("Removed {CartCount} carts and {CartItemCount} cart items for user {UserId}", carts.Count, cartItems.Count, userId);
                }

                // 2. Remove WishlistItems
                var wishlists = await _context.WishlistItems.Where(w => w.UserId == userId).ToListAsync();
                if (wishlists.Any())
                {
                    _context.WishlistItems.RemoveRange(wishlists);
                    _logger.LogInformation("Removed {WishlistCount} wishlist items for user {UserId}", wishlists.Count, userId);
                }

                // 3. Remove UserAddresses
                var addresses = await _context.UserAddresses.Where(a => a.UserId == userId).ToListAsync();
                if (addresses.Any())
                {
                    _context.UserAddresses.RemoveRange(addresses);
                    _logger.LogInformation("Removed {AddressCount} addresses for user {UserId}", addresses.Count, userId);
                }

                // 4. Remove Notifications
                var notifications = await _context.Notifications.Where(n => n.UserId == userId).ToListAsync();
                if (notifications.Any())
                {
                    _context.Notifications.RemoveRange(notifications);
                    _logger.LogInformation("Removed {NotificationCount} notifications for user {UserId}", notifications.Count, userId);
                }

                // 5. Remove StockNotificationSubscriptions
                var subscriptions = await _context.StockNotificationSubscriptions.Where(s => s.UserId == userId).ToListAsync();
                if (subscriptions.Any())
                {
                    _context.StockNotificationSubscriptions.RemoveRange(subscriptions);
                    _logger.LogInformation("Removed {SubCount} stock subscriptions for user {UserId}", subscriptions.Count, userId);
                }

                // 6. Remove OtpRecords
                var otps = await _context.OtpRecords.Where(o => o.UserId == userId || (user.Email != null && o.UserId == user.Email) || (user.PhoneNumber != null && o.UserId == user.PhoneNumber)).ToListAsync();
                if (otps.Any())
                {
                    _context.OtpRecords.RemoveRange(otps);
                    _logger.LogInformation("Removed {OtpCount} OTP records for user {UserId}", otps.Count, userId);
                }

                // 7. Coupons: Unlink personal user coupons so historical coupon records stay intact without FK issues
                var coupons = await _context.Coupons.Where(c => c.UserId == userId).ToListAsync();
                foreach (var coupon in coupons)
                {
                    coupon.UserId = null;
                }
                if (coupons.Any())
                {
                    _logger.LogInformation("Unlinked {CouponCount} coupons for user {UserId}", coupons.Count, userId);
                }

                // 8. Referrals: Clear ReferrerId & ReferredUserId foreign key constraints
                var referralsAsReferrer = await _context.Referrals.Where(r => r.ReferrerId == userId).ToListAsync();
                foreach (var refItem in referralsAsReferrer)
                {
                    refItem.ReferrerId = null;
                }
                var referralsAsReferred = await _context.Referrals.Where(r => r.ReferredUserId == userId).ToListAsync();
                foreach (var refItem in referralsAsReferred)
                {
                    refItem.ReferredUserId = null;
                }
                if (referralsAsReferrer.Any() || referralsAsReferred.Any())
                {
                    _logger.LogInformation("Unlinked {ReferrerCount} referrer records and {ReferredCount} referred records for user {UserId}", referralsAsReferrer.Count, referralsAsReferred.Count, userId);
                }

                // 9. RewardTransactions: Remove transactions belonging to user
                var rewardTxs = await _context.RewardTransactions.Where(rt => rt.UserId == userId).ToListAsync();
                if (rewardTxs.Any())
                {
                    _context.RewardTransactions.RemoveRange(rewardTxs);
                    _logger.LogInformation("Removed {RewardTxCount} reward transactions for user {UserId}", rewardTxs.Count, userId);
                }

                // 10. ProductReviews: Remove reviews created by user
                var reviews = await _context.ProductReviews.Where(r => r.UserId == userId).ToListAsync();
                if (reviews.Any())
                {
                    _context.ProductReviews.RemoveRange(reviews);
                    _logger.LogInformation("Removed {ReviewCount} reviews for user {UserId}", reviews.Count, userId);
                }

                // 11. ReturnRequests: Remove user return requests
                var returnRequests = await _context.ReturnRequests.Where(rr => rr.UserId == userId).ToListAsync();
                if (returnRequests.Any())
                {
                    _context.ReturnRequests.RemoveRange(returnRequests);
                    _logger.LogInformation("Removed {ReturnCount} return requests for user {UserId}", returnRequests.Count, userId);
                }

                await _context.SaveChangesAsync();

                // 12. Delete Identity User (removes user from AspNetUsers)
                var deleteResult = await _userManager.DeleteAsync(user);
                if (!deleteResult.Succeeded)
                {
                    var errors = string.Join(", ", deleteResult.Errors.Select(e => e.Description));
                    _logger.LogError("UserManager.DeleteAsync failed for user {UserId}: {Errors}", userId, errors);
                    if (createdTransaction != null)
                    {
                        await createdTransaction.RollbackAsync();
                    }
                    return false;
                }

                if (createdTransaction != null)
                {
                    await createdTransaction.CommitAsync();
                }
                _logger.LogInformation("Account deletion process completed successfully for user {UserId}", userId);
                return true;
            }
            catch (Exception ex)
            {
                if (createdTransaction != null)
                {
                    await createdTransaction.RollbackAsync();
                }
                _logger.LogError(ex, "Account deletion process failed and was rolled back for user {UserId}", userId);
                throw;
            }
            finally
            {
                if (createdTransaction != null)
                {
                    await createdTransaction.DisposeAsync();
                }
            }
        }
    }
}
