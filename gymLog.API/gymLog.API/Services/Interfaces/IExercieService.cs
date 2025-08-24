using gymLog.API.Model.DTO;
using gymLog.API.Model.DTO.ExerciseDto;

namespace gymLog.API.Services.Interfaces
{
    public interface IExerciseService
    {
        public Task<Result<IEnumerable<ExerciseDto>>> GetAllExercises(Guid workoutDayId);
        public Task<Result<ExerciseDto>> CreateExercise(CreateExerciseDto createExerciseDto);
        public Task<Result<ExerciseDto>> UpdateExercise(Guid exerciseId, CreateExerciseDto createExerciseDto);
        public Task<Result<bool>> RemoveExercise(Guid exerciseId);

    }
}