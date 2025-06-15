using gymLog.API.Services.interfaces;
using gymLog.Model;
using Microsoft.AspNetCore.Mvc;

namespace gymLog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutPlanController : ControllerBase
    {
        private readonly IWorkoutPlanService _workoutPlanService;
        private readonly ILogService _logService;

        public WorkoutPlanController(IWorkoutPlanService workoutPlanService, ILogService logService)
        {
            _workoutPlanService = workoutPlanService;
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutPlan>>> GetWorkoutPlans()
        {
            _logService.LogInfo("Received request to get all workout plans");
            try
            {
                var workoutPlans = await _workoutPlanService.GetAllAsync();
                _logService.LogInfo("Successfully retrieved {count} workout plans", workoutPlans.Count());
                return Ok(workoutPlans);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, "Error occurred while getting all workout plans");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutPlan>> GetWorkoutPlan(Guid id)
        {
            _logService.LogInfo("Received request to get workout plan with ID {id}", id);
            try
            {
                var workoutPlan = await _workoutPlanService.GetByIdAsync(id);

                if (workoutPlan == null)
                {
                    _logService.LogWarning("Workout plan with ID {id} not found", id);
                    return NotFound();
                }

                _logService.LogInfo("Successfully retrieved workout plan with ID {id}", id);
                return Ok(workoutPlan);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, "Error occurred while getting workout plan with ID {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkoutPlan(Guid id, WorkoutPlan workoutPlan)
        {
            _logService.LogInfo("Received request to update workout plan with ID {id}", id);
            try
            {
                if (id != workoutPlan.Id)
                {
                    _logService.LogWarning("WorkoutPlan ID in URL ({urlId}) does not match workoutPlan.Id ({workoutPlanId}) in body", id, workoutPlan.Id);
                    return BadRequest();
                }

                var updatedWorkoutPlan = await _workoutPlanService.UpdateAsync(workoutPlan);
                _logService.LogInfo("Successfully updated workout plan with ID {id}", id);
                return Ok(updatedWorkoutPlan);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, "Error occurred while updating workout plan with ID {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<WorkoutPlan>> PostWorkoutPlan(WorkoutPlan workoutPlan)
        {
            _logService.LogInfo("Received request to create a new workout plan");
            try
            {
                var createdWorkoutPlan = await _workoutPlanService.CreateAsync(workoutPlan);
                _logService.LogInfo("Successfully created new workout plan with ID {id}", createdWorkoutPlan.Id);
                return CreatedAtAction(nameof(GetWorkoutPlan), new { id = createdWorkoutPlan.Id }, createdWorkoutPlan);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, "Error occurred while creating a new workout plan");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkoutPlan(Guid id)
        {
            _logService.LogInfo("Received request to delete workout plan with ID {id}", id);
            try
            {
                var result = await _workoutPlanService.DeleteAsync(id);
                if (!result)
                {
                    _logService.LogWarning("Failed to delete workout plan with ID {id}, not found", id);
                    return NotFound();
                }

                _logService.LogInfo("Successfully deleted workout plan with ID {id}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, "Error occurred while deleting workout plan with ID {id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
