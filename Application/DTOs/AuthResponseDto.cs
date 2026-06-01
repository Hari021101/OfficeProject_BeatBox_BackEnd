namespace Application.DTOs
{
    public class AuthResponseDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public IList<string> Roles { get; set; } = new List<string>();
    }

    /// <summary>
    /// Returned after initial registration — JWT not issued yet, OTP verification required.
    /// </summary>
    public class RegisterResponseDto
    {
        public string UserId { get; set; } = string.Empty;
        public string Identifier { get; set; } = string.Empty;
        public string IdentifierType { get; set; } = string.Empty; // "email" or "phone"
        public string Message { get; set; } = string.Empty;
    }

    public class OtpVerifyDto
    {
        public string UserId { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }

    public class SendPhoneOtpDto
    {
        public string UserId { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
