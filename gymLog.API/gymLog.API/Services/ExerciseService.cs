using gymLog.API.Model;
using gymLog.API.Services.interfaces;
using gymLog.Entity;
using gymLog.Model;

namespace gymLog.API.Services
{
    public class ExerciseService(AppDbContext context) : BasicCrudService<gymLog.Model.Exercise>(context), IExerciseService
    {
        public override async Task<Result<gymLog.Model.Exercise>> CreateAsync(gymLog.Model.Exercise exercise) {
            if (string.IsNullOrEmpty(exercise.Name))
                return Result<gymLog.Model.Exercise>.Failure("Exercise has no name", null);
            
            if (exercise.WorkoutDay == null)
                return Result<gymLog.Model.Exercise>.Failure("Exercise has no workout day", null);
            
            if (exercise.Category == null)
                return Result<gymLog.Model.Exercise>.Failure("Exercise has no category", null);
            
            return await base.CreateAsync(exercise);
        }

        public override async Task<Result<gymLog.Model.Exercise?>> UpdateAsync(gymLog.Model.Exercise exercise) {
            if (GetByIdAsync(exercise.Id).Result.Data == null)
                return Result<gymLog.Model.Exercise?>.Failure("Exercise does not exist", null);
            
            if (string.IsNullOrEmpty(exercise.Name))
                return Result<gymLog.Model.Exercise?>.Failure("Exercise has no name", null);
            
            if (exercise.WorkoutDay == null)
                return Result<gymLog.Model.Exercise?>.Failure("Exercise has no workout day", null);
            
            if (exercise.Category == null)
                return Result<gymLog.Model.Exercise?>.Failure("Exercise has no category", null);
            
            return await base.UpdateAsync(exercise);
        }
    }
}