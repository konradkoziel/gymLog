using AutoMapper;
using gymLog.API.Entity;
using gymLog.API.Model;
using gymLog.API.Model.DTO;
using gymLog.API.Model.DTO.ExerciseDto;
using gymLog.API.Services.Interfaces;
using gymLog.API.Validators;
using Microsoft.EntityFrameworkCore;

namespace gymLog.API.Services;

public class ExerciseService : IExerciseService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ExerciseService(AppDbContext context, IMapper mapper, ILogService logService)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<Result<IEnumerable<ExerciseDto>>> GetAllExercises(Guid workoutDayId)
    {
        var exercises = await _context.Exercises.Where(e => e.WorkoutDayId == workoutDayId).ToListAsync();
        if (exercises.Count == 0) return Result<IEnumerable<ExerciseDto>>.Failure("Not found");
        var execricesDto = _mapper.Map<List<ExerciseDto>>(exercises);
        return Result<IEnumerable<ExerciseDto>>.Success(execricesDto);
    }

    public async Task<Result<ExerciseDto>> CreateExercise(CreateExerciseDto createExerciseDto)
    {
        var validator = new ExerciseValidator();
        validator.Validate(createExerciseDto);
        var exercise = _mapper.Map<Exercise>(createExerciseDto);
        _context.Add((object)exercise);
        await _context.SaveChangesAsync();
        return Result<ExerciseDto>.Success(_mapper.Map<ExerciseDto>(exercise));
    }

    public async Task<Result<ExerciseDto>> UpdateExercise(Guid exerciseId, CreateExerciseDto createExerciseDto)
    {
        var exercise = await _context.Exercises.FindAsync(exerciseId);
        if (exercise == null) return Result<ExerciseDto>.Failure("Not found");
        _mapper.Map(createExerciseDto, exercise);
        _context.Exercises.Update(exercise);
        await _context.SaveChangesAsync();
        return Result<ExerciseDto>.Success(_mapper.Map<ExerciseDto>(exercise));
    }

    public async Task<Result<bool>> RemoveExercise(Guid exerciseId)
    {
        var exercise = await _context.Exercises.FindAsync(exerciseId);
        if (exercise == null) return Result<bool>.Failure("Not found");
        _context.Exercises.Remove(exercise);
        await _context.SaveChangesAsync();
        return Result<bool>.Success(true);
    }
}