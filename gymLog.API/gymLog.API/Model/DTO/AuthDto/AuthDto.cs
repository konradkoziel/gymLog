﻿namespace gymLog.API.Model.DTO.AuthDto
{
    public class AuthDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public DateTimeOffset AccessTokenExpiresAt { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTimeOffset RefreshTokenExpiresAt { get; set; }
    }

}
