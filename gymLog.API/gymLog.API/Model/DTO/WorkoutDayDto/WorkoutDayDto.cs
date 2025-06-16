namespace gymLog.API.Model.DTO.WorkoutDayDto;

public class WorkoutDayDto
{
    public Guid Id { get; set; }
    public int DayOfWeek { get; set; }
    public string? Notes { get; set; }
}