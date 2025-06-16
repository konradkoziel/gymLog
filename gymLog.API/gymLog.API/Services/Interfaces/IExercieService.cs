using gymLog.API.Model.DTO;
using gymLog.API.Model.DTO.ExerciseDto;

namespace gymLog.API.Services.interfaces
{
    public interface IExerciseService
    {
        public Task<Result<IEnumerable<ExerciseDto>>> GetAllExercises(Guid workoutDayId);
        public Task<Result<ExerciseDto>> CreateExercise(Guid workoutDayId, ExerciseDto exerciseDto);
        public Task<Result<ExerciseDto>> UpdateExercise(Guid exerciseId, ExerciseDto exerciseDto);
        public Task<Result<bool>> RemoveExercise(Guid exerciseId);

    }
}