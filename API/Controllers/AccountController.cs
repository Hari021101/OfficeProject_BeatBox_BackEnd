using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public AccountController(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ITokenService tokenService,
            IOtpService otpService,
            IAuditLogService auditLogService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _otpService = otpService;
            _auditLogService = auditLogService;
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
    }
}
