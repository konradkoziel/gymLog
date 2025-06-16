namespace gymLog.API.Model.DTO.WorkoutDayDto;

public class CreateWorkoutDayDto
{
    public int DayOfWeek { get; set; } // 0-6
    public string? Notes { get; set; }
}