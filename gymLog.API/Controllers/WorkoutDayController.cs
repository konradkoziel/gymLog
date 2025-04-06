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
    public class WorkoutDayController : ControllerBase
    {
        private readonly IWorkoutDayService _workoutDayService;

        public WorkoutDayController(IWorkoutDayService workoutDayService)
        {
            _workoutDayService = workoutDayService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutDay>>> GetWorkoutDays()
        {
            var workoutDays = await _workoutDayService.GetAllAsync();
            return Ok(workoutDays);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutDay>> GetWorkoutDay(Guid id)
        {
            var workoutDay = await _workoutDayService.GetByIdAsync(id);

            if (workoutDay == null)
            {
                return NotFound();
            }

            return Ok(workoutDay);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkoutDay(Guid id, WorkoutDay workoutDay)
        {
            if (id != workoutDay.Id)
            {
                return BadRequest();
            }

            var updatedWorkoutDay = await _workoutDayService.UpdateAsync(workoutDay);
            return Ok(updatedWorkoutDay);
        }

        [HttpPost]
        public async Task<ActionResult<WorkoutDay>> PostWorkoutDay(WorkoutDay workoutDay)
        {
            var createdWorkoutDay = await _workoutDayService.CreateAsync(workoutDay);
            return CreatedAtAction(nameof(GetWorkoutDay), new { id = createdWorkoutDay.Id }, createdWorkoutDay);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkoutDay(Guid id)
        {
            var result = await _workoutDayService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
