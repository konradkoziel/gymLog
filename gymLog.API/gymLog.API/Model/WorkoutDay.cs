namespace gymLog.API.Model;

public class WorkoutDay
{
    public Guid Id { get; set; }
    public Guid WorkoutPlanId { get; set; }
    public WorkoutPlan WorkoutPlan { get; set; } = null!;
    public int DayOfWeek { get; set; } // 0-6 (Sunday-Saturday)
    public string? Notes { get; set; }
    public List<Exercise> Exercises { get; set; } = new List<Exercise>();
}