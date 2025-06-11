using gymLog.Model;
using gymLog.API.Services.interfaces;

namespace gymLog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseService _service;
        private readonly ILogService _logService;

        public ExerciseController(IExerciseService exerciseService, ILogService logService)
        {
            _service = exerciseService;
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exercise>>> GetExercises()
        {
            _logService.LogInfo("Getting all exercises");
            try
            {
                var exercises = await _service.GetAllAsync();
                _logService.LogInfo("Got {count} exercises", exercises.Count ?? 0);
                return Ok(exercises);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, "Error while getting all exercises");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Exercise>> GetExercise(Guid id)
        {
            _logService.LogInfo("Getting exercise with ID {id}", id);
            try
            {
                var exercise = await _service.GetByIdAsync(id);

                if (exercise == null)
                {
                    _logService.LogWarning("Exercise with ID {id} not found", id);
                    return NotFound();
                }

                return Ok(exercise);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, "Error while getting exercise with ID {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutExercise(Guid id, Exercise exercise)
        {
            _logService.LogInfo("Updating exercise with ID {id}", id);
            try
            {
                if (id != exercise.Id)
                {
                    _logService.LogWarning("ID in URL ({urlId}) doesn't match exercise.Id ({exerciseId})", id, exercise.Id);
                    return BadRequest();
                }

                var updated = await _service.UpdateAsync(exercise);
                _logService.LogInfo("Exercise with ID {id} updated", id);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, "Error while updating exercise with ID {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Exercise>> PostExercise(Exercise exercise)
        {
            _logService.LogInfo("Creating new exercise");
            try
            {
                var created = await _service.CreateAsync(exercise);
                _logService.LogInfo("Exercise created with ID {id}", created.Id);
                return CreatedAtAction(nameof(GetExercise), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, "Error while creating exercise");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercise(Guid id)
        {
            _logService.LogInfo("Deleting exercise with ID {id}", id);
            try
            {
                var result = await _service.DeleteAsync(id);
                if (!result)
                {
                    _logService.LogWarning("Exercise with ID {id} not found for deletion", id);
                    return NotFound();
                }

                _logService.LogInfo("Exercise with ID {id} deleted", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, "Error while deleting exercise with ID {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
