using AutoMapper;
using gymLog.API.Entity;
using gymLog.API.Model;
using gymLog.API.Model.DTO;
using gymLog.API.Model.DTO.WorkoutDayDto;
using gymLog.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gymLog.API.Services;

public class WorkoutDayService(AppDbContext context, IMapper mapper) : IWorkoutDayService
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    
    public async Task<Result<IEnumerable<WorkoutDayDto>>> GetAllWorkoutDays(Guid workoutPlanId)
    {
        var workoutDays = await _context.WorkoutDays.Where(wd => wd.WorkoutPlanId == workoutPlanId).ToListAsync();
        if (workoutDays.Count == 0) return Result<IEnumerable<WorkoutDayDto>>.Failure("Not found");
        var workoutDaysDto = _mapper.Map<List<WorkoutDayDto>>(workoutDays);
        return Result<IEnumerable<WorkoutDayDto>>.Success(workoutDaysDto);
    }

    public async Task<Result<WorkoutDayDto>> GetWorkoutDayById(Guid workoutDayId)
    {
        var workoutDay = await _context.WorkoutDays.FindAsync(workoutDayId);
        if (workoutDay == null) return Result<WorkoutDayDto>.Failure("Not found");
        var workoutDayDto = _mapper.Map<WorkoutDayDto>(workoutDay);
        return Result<WorkoutDayDto>.Success(workoutDayDto);
    }

    public async Task<Result<WorkoutDayDto>> CreateWorkoutDay(Guid workoutPlanId, CreateWorkoutDayDto createDayDto)
    {
        var workoutDay = _mapper.Map<WorkoutDay>(createDayDto);
        workoutDay.WorkoutPlanId = workoutPlanId;
        _context.Add((object)workoutDay);
        await _context.SaveChangesAsync();
        return Result<WorkoutDayDto>.Success(_mapper.Map<WorkoutDayDto>(workoutDay));
    }

    public async Task<Result<WorkoutDayDto>> UpdateWorkoutDay(Guid workoutDayId, CreateWorkoutDayDto createDayDto)
    {
        var workoutDay = await _context.WorkoutDays.FindAsync(workoutDayId);
        
        if (workoutDay == null) return Result<WorkoutDayDto>.Failure("Not found");
        
        _mapper.Map(createDayDto, workoutDay);
        _context.WorkoutDays.Update(workoutDay);
        await _context.SaveChangesAsync();
        
        return Result<WorkoutDayDto>.Success(_mapper.Map<WorkoutDayDto>(workoutDay));
    }

    public async Task<Result<bool>> RemoveWorkoutDay(Guid workoutDayId)
    {
        var workoutDay = await _context.WorkoutDays.FindAsync(workoutDayId);
        
        if (workoutDay == null) return Result<bool>.Failure("Not found");
        
        _context.WorkoutDays.Remove(workoutDay);
        await _context.SaveChangesAsync();
        
        return Result<bool>.Success(true);
    }
}