using gymLog.API.Model;
using gymLog.API.Model.DTO.ExerciseDto;
using Microsoft.AspNetCore.Mvc;
using gymLog.API.Services.interfaces;

namespace gymLog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseService _service;

        public ExerciseController(IExerciseService exerciseService, ILogService logService)
        {
            _service = exerciseService;
        }

        [HttpGet("{workoutDayId:guid}")]
        public async Task<ActionResult<IEnumerable<Exercise>>> GetExercises(Guid workoutDayId)
        {
            var result = await _service.GetAllExercises(workoutDayId);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("{exerciseId:guid}")]
        public async Task<IActionResult> PutExercise(Guid exerciseId, ExerciseDto exerciseDto)
        {
            var result = await _service.UpdateExercise(exerciseId, exerciseDto);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("{workoutDayId:guid}")]
        public async Task<ActionResult<Exercise>> PostExercise(Guid workoutDayId, ExerciseDto exerciseDto)
        {
            var result = await _service.CreateExercise(workoutDayId, exerciseDto);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("{exerciseId:guid}")]
        public async Task<IActionResult> DeleteExercise(Guid exerciseId)
        {
            var result = await _service.RemoveExercise(exerciseId);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        
    }
}