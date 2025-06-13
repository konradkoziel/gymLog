using gymLog.API.Model.DTO;
using gymLog.API.Services.interfaces;
using gymLog.Entity;
using gymLog.Model;
using Microsoft.EntityFrameworkCore;

namespace gymLog.API.Services
{
    public class ExerciseService(AppDbContext context) : BasicCrudService<Exercise>(context), IExerciseService
    {
        public override async Task<Result<Exercise>> CreateAsync(Exercise exercise) {
            if (string.IsNullOrEmpty(exercise.Name))
                return Result<Exercise>.Failure("Exercise has no name", null);
            
            if (exercise.WorkoutDay == null)
                return Result<Exercise>.Failure("Exercise has no workout day", null);
            
            if (exercise.Category == null)
                return Result<Exercise>.Failure("Exercise has no category", null);
            
            return await base.CreateAsync(exercise);
        }
    }
}