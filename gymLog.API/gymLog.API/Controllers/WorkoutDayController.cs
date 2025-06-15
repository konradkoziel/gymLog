using gymLog.API.Services;
using gymLog.API.Services.interfaces;
using gymLog.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gymLog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutDayController : ControllerBase {
        private readonly IWorkoutDayService _workoutDayService;
        private readonly ILogService _logService;

        public WorkoutDayController(IWorkoutDayService workoutDayService, ILogService logService) {
            _workoutDayService = workoutDayService;
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutDay>>> GetWorkoutDays() {
            _logService.LogInfo("Received request to get all workout days");
            try {
                var workoutDaysResult = await _workoutDayService.GetAllAsync();
                if (workoutDaysResult.IsSuccess) {
                    _logService.LogInfo("Successfully retrieved {count} workout days", workoutDaysResult.Data!.Count());
                    return Ok(workoutDaysResult);
                } else {
                    return NotFound();
                }

            } catch (Exception ex) {
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
                var workoutDayResult = await _workoutDayService.GetByIdAsync(id);

                if (workoutDayResult.Data == null)
                {
                    _logService.LogWarning("Workout day with ID {id} not found", id);
                    return BadRequest();
                }

                _logService.LogInfo("Successfully retrieved workout day with ID {id}", id);
                return Ok(workoutDayResult);
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

                var updatedWorkoutDayResult = await _workoutDayService.UpdateAsync(workoutDay);
                if (updatedWorkoutDayResult.IsSuccess) {
                    _logService.LogInfo("Successfully updated workout day with ID {id}", id);
                    return Ok(updatedWorkoutDayResult);
                } else {
                    return BadRequest();
                }
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
                var createdWorkoutDayResult = await _workoutDayService.CreateAsync(workoutDay);
                if (createdWorkoutDayResult.IsSuccess) {
                    _logService.LogInfo("Successfully created new workout day with ID {id}", createdWorkoutDayResult.Data!.Id);
                    return CreatedAtAction(nameof(GetWorkoutDay), new { id = createdWorkoutDayResult.Data!.Id }, createdWorkoutDayResult);
                } else {
                    return BadRequest();
                }
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
                if (!result.IsSuccess)
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
