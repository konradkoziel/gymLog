namespace gymLog.API.Model.DTO.AuthDto
{
    public class RegisterDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTimeOffset? DateOfBirth { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Height { get; set; }
    }
}
