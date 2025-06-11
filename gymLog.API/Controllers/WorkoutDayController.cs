using gymLog.API.Services;
using gymLog.API.Services.interfaces;
using gymLog.Model;

namespace gymLog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutDayController : ControllerBase
    {
        private readonly IWorkoutDayService _workoutDayService;
        private readonly ILogService _logService;

        public WorkoutDayController(IWorkoutDayService workoutDayService, ILogService logService)
        {
            _workoutDayService = workoutDayService;
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutDay>>> GetWorkoutDays()
        {
            _logService.LogInfo("Received request to get all workout days");
            try
            {
                var workoutDays = await _workoutDayService.GetAllAsync();
                _logService.LogInfo("Successfully retrieved {count} workout days", workoutDays.Count ?? 0);
                return Ok(workoutDays);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, "Error occurred while getting all workout days");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutDay>> GetWorkoutDay(Guid id)
        {
            _logService.LogInfo("Received request to get workout day with ID {id}", id);
            try
            {
                var workoutDay = await _workoutDayService.GetByIdAsync(id);

                if (workoutDay == null)
                {
                    _logService.LogWarning("Workout day with ID {id} not found", id);
                    return NotFound();
                }

                _logService.LogInfo("Successfully retrieved workout day with ID {id}", id);
                return Ok(workoutDay);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, "Error occurred while getting workout day with ID {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkoutDay(Guid id, WorkoutDay workoutDay)
        {
            _logService.LogInfo("Received request to update workout day with ID {id}", id);
            try
            {
                if (id != workoutDay.Id)
                {
                    _logService.LogWarning("WorkoutDay ID in URL ({urlId}) does not match workoutDay.Id ({workoutDayId}) in body", id, workoutDay.Id);
                    return BadRequest();
                }

                var updatedWorkoutDay = await _workoutDayService.UpdateAsync(workoutDay);
                _logService.LogInfo("Successfully updated workout day with ID {id}", id);
                return Ok(updatedWorkoutDay);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, "Error occurred while updating workout day with ID {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<WorkoutDay>> PostWorkoutDay(WorkoutDay workoutDay)
        {
            _logService.LogInfo("Received request to create a new workout day");
            try
            {
                var createdWorkoutDay = await _workoutDayService.CreateAsync(workoutDay);
                _logService.LogInfo("Successfully created new workout day with ID {id}", createdWorkoutDay.Id);
                return CreatedAtAction(nameof(GetWorkoutDay), new { id = createdWorkoutDay.Id }, createdWorkoutDay);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, "Error occurred while creating a new workout day");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkoutDay(Guid id)
        {
            _logService.LogInfo("Received request to delete workout day with ID {id}", id);
            try
            {
                var result = await _workoutDayService.DeleteAsync(id);
                if (!result)
                {
                    _logService.LogWarning("Failed to delete workout day with ID {id}, not found", id);
                    return NotFound();
                }

                _logService.LogInfo("Successfully deleted workout day with ID {id}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, "Error occurred while deleting workout day with ID {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
