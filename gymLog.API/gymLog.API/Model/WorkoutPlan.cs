namespace gymLog.API.Model;

public class WorkoutPlan
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<WorkoutDay> Days { get; set; } = new List<WorkoutDay>();
}