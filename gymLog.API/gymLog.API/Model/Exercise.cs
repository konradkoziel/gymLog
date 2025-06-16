namespace gymLog.API.Model;

public class Exercise
{
    public Guid Id { get; set; }
    public Guid WorkoutDayId { get; set; }
    public WorkoutDay WorkoutDay { get; set; } = null!;

    public string Name { get; set; } = string.Empty;
    public int Reps { get; set; }
    public decimal LastMaxWeight { get; set; }
    public int Order { get; set; }

    public List<ExerciseProgress> ProgressHistory { get; set; } = new();
}