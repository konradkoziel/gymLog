using gymLog.API.Model.DTO;
using gymLog.API.Model.DTO.WorkoutDayDto;

namespace gymLog.API.Services.Interfaces;

public interface IWorkoutPlanService
{
    public Task<Result<IEnumerable<PlanDto>>> GetAllWorkoutPlans(Guid userId);
    public Task<Result<PlanDto>> GetWorkoutPlanById(Guid workoutPlanId);
    public Task<Result<PlanDto>> CreateWorkoutPlan(Guid userId, CreatePlanDto createPlanDto);
    public Task<Result<PlanDto>> UpdateWorkoutPlan(Guid workoutPlanId, CreatePlanDto createPlanDto);
    public Task<Result<bool>> RemoveWorkoutPlan(Guid workoutId);
}