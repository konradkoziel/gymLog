namespace gymLog.API.Model;

public class ExerciseProgress
{
    public Guid Id { get; set; }
    public Guid ExerciseId { get; set; }
    public Exercise Exercise { get; set; } = null!;
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public decimal WeightUsed { get; set; }
    public int RepsDone { get; set; }
}