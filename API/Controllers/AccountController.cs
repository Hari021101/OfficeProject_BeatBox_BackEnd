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

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, IOtpService otpService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _otpService = otpService;
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

                result.Add(new
                {
                    id = user.Id,
                    fullName = user.FullName,
                    email = user.Email,
                    phoneNumber = user.PhoneNumber,
                    roles = roles,
                    isActive = true,
                    joinDate = user.CreatedDate // or CreatedAt if exists
                });
            }

            return Ok(result);
        }
    }
}
