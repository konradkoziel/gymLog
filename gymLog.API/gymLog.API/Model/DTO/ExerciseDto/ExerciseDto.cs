namespace gymLog.API.Model.DTO.ExerciseDto;

public class ExerciseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Reps { get; set; }
    public decimal LastMaxWeight { get; set; }
    public int Order { get; set; }
}