namespace gymLog.API.Model.DTO.ExerciseProgressDto;

public class ExerciseProgressDto
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public decimal WeightUsed { get; set; }
    public int RepsDone { get; set; }
}