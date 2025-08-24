using gymLog.API.Model.DTO;
using gymLog.API.Model.DTO.ExerciseDto;
using gymLog.API.Model.DTO.WorkoutDayDto;

namespace gymLog.API.Services.Interfaces;

public interface IWorkoutDayService
{
    public Task<Result<IEnumerable<WorkoutDayDto>>> GetAllWorkoutDays(Guid workoutPlanId);
    public Task<Result<WorkoutDayDto>> GetWorkoutDayById(Guid workoutDayId);
    public Task<Result<WorkoutDayDto>> CreateWorkoutDay(Guid workoutPlanId, CreateWorkoutDayDto createDayDto);
    public Task<Result<WorkoutDayDto>> UpdateWorkoutDay(Guid workoutDayId, CreateWorkoutDayDto createDayDto);
    public Task<Result<bool>> RemoveWorkoutDay(Guid workoutDayId);
}