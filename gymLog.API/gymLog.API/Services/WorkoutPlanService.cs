using AutoMapper;
using gymLog.API.Entity;
using gymLog.API.Model;
using gymLog.API.Model.DTO;
using gymLog.API.Model.DTO.ExerciseDto;
using gymLog.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gymLog.API.Services;

public class WorkoutPlanService(AppDbContext context, IMapper mapper) : IWorkoutPlanService
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    
    public async Task<Result<IEnumerable<PlanDto>>> GetAllWorkoutPlans(Guid userId)
    {
        var workoutPlans = await _context.WorkoutPlans.Where(wp => wp.UserId == userId).ToListAsync();
        if (workoutPlans.Count == 0) return Result<IEnumerable<PlanDto>>.Failure("Not found");
        var workoutPlansDto = _mapper.Map<List<PlanDto>>(workoutPlans);
        return Result<IEnumerable<PlanDto>>.Success(workoutPlansDto);
    }

    public async Task<Result<PlanDto>> GetWorkoutPlanById(Guid workoutPlanId)
    {
        var workoutPlan = await _context.WorkoutPlans.FindAsync(workoutPlanId);
        if (workoutPlan == null) return Result<PlanDto>.Failure("Not found");
        var workoutPlanDto = _mapper.Map<PlanDto>(workoutPlan);
        return Result<PlanDto>.Success(workoutPlanDto);
    }

    public async Task<Result<PlanDto>> CreateWorkoutPlan(Guid userId, CreatePlanDto createPlanDto)
    {
        var workoutPlan = _mapper.Map<WorkoutPlan>(createPlanDto);
        workoutPlan.UserId = userId;
        _context.Add((object)workoutPlan);
        await _context.SaveChangesAsync();
        return Result<PlanDto>.Success(_mapper.Map<PlanDto>(workoutPlan));
    }

    public async Task<Result<PlanDto>> UpdateWorkoutPlan(Guid workoutPlanId, CreatePlanDto createPlanDto)
    {
        var workoutPlan = await _context.WorkoutPlans.FindAsync(workoutPlanId);
        
        if (workoutPlan == null) return Result<PlanDto>.Failure("Not found");
        
        _mapper.Map(createPlanDto, workoutPlan);
        _context.WorkoutPlans.Update(workoutPlan);
        await _context.SaveChangesAsync();
        
        return Result<PlanDto>.Success(_mapper.Map<PlanDto>(workoutPlan));
    }

    public async Task<Result<bool>> RemoveWorkoutPlan(Guid workoutId)
    {
        var workoutPlan = await _context.WorkoutPlans.FindAsync(workoutId);
        
        if (workoutPlan == null) return Result<bool>.Failure("Not found");
        
        _context.WorkoutPlans.Remove(workoutPlan);
        await _context.SaveChangesAsync();
        
        return Result<bool>.Success(true);
    }
}