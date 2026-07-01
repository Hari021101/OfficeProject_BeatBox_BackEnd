using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IOtpService _otpService;
        private readonly IBusinessEventPublisher _eventPublisher;

        public AccountController(
            UserManager<AppUser> userManager, 
            ITokenService tokenService, 
            IOtpService otpService,
            IBusinessEventPublisher eventPublisher)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _otpService = otpService;
            _eventPublisher = eventPublisher;
        }

        // POST /api/account/register
        // Creates user (unverified) and sends email OTP — JWT issued only after OTP verification
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
                IsPhoneVerified = false
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(error.Code, error.Description);
                return BadRequest(ModelState);
            }

            // Send corresponding OTP
            if (isEmail)
            {
                await _otpService.SendEmailOtpAsync(user.Id, user.Email!);
            }
            else
            {
                await _otpService.SendPhoneOtpAsync(user.Id, user.PhoneNumber!);
            }

            // Log event via IBusinessEventPublisher
            await _eventPublisher.PublishAsync(new Application.Common.Events.BusinessEvent
            {
                ActionType = "REGISTERED",
                EntityType = "User",
                EntityId = user.Id,
                Title = user.Email ?? user.PhoneNumber ?? "User",
                Description = $"User account registered (Identifier: {registerDto.Identifier})",
                Icon = "UserPlus",
                ColorClass = "text-success",
                BgClass = "bg-success"
            });

            return Ok(new RegisterResponseDto
            {
                UserId = user.Id,
                Identifier = registerDto.Identifier,
                IdentifierType = isEmail ? "email" : "phone",
                Message = $"Account created. Please check your {(isEmail ? "email" : "phone")} for the verification code."
            });
        }

        // POST /api/account/login
        // Accepts email address OR phone number as the identifier
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto loginDto)
        {
            AppUser? user = null;

            if (loginDto.Identifier.Contains('@'))
            {
                // Treat as email
                user = await _userManager.FindByEmailAsync(loginDto.Identifier);
            }
            else
            {
                // Treat as phone number — search all users
                user = _userManager.Users.FirstOrDefault(u => u.PhoneNumber == loginDto.Identifier);
            }

            if (user == null) return Unauthorized("Invalid credentials.");

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result) return Unauthorized("Invalid credentials.");

            var roles = await _userManager.GetRolesAsync(user);
            var isAdmin = roles.Contains("Admin");

            // Log event via IBusinessEventPublisher
            await _eventPublisher.PublishAsync(new Application.Common.Events.BusinessEvent
            {
                ActionType = isAdmin ? "ADMIN_LOGIN" : "LOGIN",
                EntityType = "User",
                EntityId = user.Id,
                Title = user.Email ?? user.UserName ?? "User",
                Description = isAdmin ? $"Administrator logged in: {user.FullName}" : $"User logged in: {user.FullName}",
                Icon = isAdmin ? "Shield" : "LogIn",
                ColorClass = isAdmin ? "text-danger" : "text-info",
                BgClass = isAdmin ? "bg-danger" : "bg-info"
            });

            return new AuthResponseDto
            {
                FullName = user.FullName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                Token = await _tokenService.CreateToken(user),
                Roles = roles
            };
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = _userManager.Users.ToList();

            var result = new List<object>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var isLocked = await _userManager.IsLockedOutAsync(user);

                result.Add(new
                {
                    id = user.Id,
                    fullName = user.FullName,
                    email = user.Email,
                    phoneNumber = user.PhoneNumber,
                    roles = roles,
                    isActive = !isLocked,
                    joinDate = user.CreatedDate
                });
            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("users/{userId}/role")]
        public async Task<IActionResult> UpdateUserRole(string userId, [FromBody] UpdateRoleDto dto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound("User not found.");

            var currentRoles = await _userManager.GetRolesAsync(user);
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded) return BadRequest("Failed to remove current roles.");

            var addResult = await _userManager.AddToRoleAsync(user, dto.Role);
            if (!addResult.Succeeded) return BadRequest("Failed to assign new role.");

            // Log event via IBusinessEventPublisher
            await _eventPublisher.PublishAsync(new Application.Common.Events.BusinessEvent
            {
                ActionType = "UPDATED",
                EntityType = "User",
                EntityId = user.Id,
                Title = user.Email ?? user.UserName ?? "User",
                Description = $"Role changed to {dto.Role} by Administrator",
                Icon = "ShieldAlert",
                ColorClass = "text-warning",
                BgClass = "bg-warning"
            });

            return Ok(new { message = "Role updated successfully." });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("users/{userId}/lock")]
        public async Task<IActionResult> LockUser(string userId, [FromBody] LockUserDto dto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound("User not found.");

            if (dto.LockUser)
            {
                // Lock account for 100 years
                await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddYears(100));
            }
            else
            {
                // Unlock account
                await _userManager.SetLockoutEndDateAsync(user, null);
            }

            // Log event via IBusinessEventPublisher
            await _eventPublisher.PublishAsync(new Application.Common.Events.BusinessEvent
            {
                ActionType = dto.LockUser ? "LOCKED" : "UNLOCKED",
                EntityType = "User",
                EntityId = user.Id,
                Title = user.Email ?? user.UserName ?? "User",
                Description = dto.LockUser ? "User account locked by Administrator" : "User account unlocked by Administrator",
                Icon = "ShieldAlert",
                ColorClass = dto.LockUser ? "text-danger" : "text-success",
                BgClass = dto.LockUser ? "bg-danger" : "bg-success"
            });

            return Ok(new { message = dto.LockUser ? "User account locked." : "User account unlocked." });
        }
    }

    public class UpdateRoleDto
    {
        public string Role { get; set; } = string.Empty;
    }

    public class LockUserDto
    {
        public bool LockUser { get; set; }
    }
}
