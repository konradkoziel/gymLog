using gymLog.Model;

namespace gymLog.API.Model;

public class RefreshToken {
    public Guid Id { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTimeOffset ExpiresAt { get; set; }
    public bool IsRevoked { get; set; }
    public bool IsUsed { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}