namespace gymLog.API.Model.DTO.ExerciseProgressDto;

public class CreateExerciseProgressDto
{
    public DateTime Date { get; set; }
    public decimal WeightUsed { get; set; }
    public int RepsDone { get; set; }
}