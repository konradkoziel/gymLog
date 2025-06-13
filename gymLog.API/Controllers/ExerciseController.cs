using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using gymLog.Model;
using gymLog.API.Services.interfaces;

namespace gymLog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseService _service;

        public ExerciseController(IExerciseService exerciseService)
        {
            _service = exerciseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exercise>>> GetExercises()
        {
            var exercises = await _service.GetAllAsync();
            return Ok(exercises);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Exercise>> GetExercise(Guid id)
        {
            var exercise = await _service.GetByIdAsync(id);

            if (exercise == null)
            {
                return NotFound();
            }

            return Ok(exercise);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutExercise(Guid id, Exercise exercise)
        {
            if (id != exercise.Id)
            {
                return BadRequest();
            }

            var updatedExercise = await _service.UpdateAsync(exercise);
            return Ok(updatedExercise);
        }

        [HttpPost]
        public async Task<ActionResult<Exercise>> PostExercise(Exercise exercise)
        {
            var createdExerciseResult = await _service.CreateAsync(exercise);
            
            if (createdExerciseResult.IsSuccess) 
                return CreatedAtAction(nameof(GetExercise), new { id = createdExerciseResult.Data!.Id }, createdExerciseResult.Data);
            
            return BadRequest(createdExerciseResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercise(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

