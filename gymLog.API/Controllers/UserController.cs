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
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService userService)
        {
            _service = userService;
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
            var user = await _service.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            var updatedUser = await _service.UpdateAsync(user);
            return Ok(updatedUser);
        }

        [HttpPost]
        public async Task<ActionResult<Exercise>> PostExercise(User user)
        {
            var createdUserResult = await _service.CreateAsync(user);
            
            if (createdUserResult.IsSuccess) 
                return CreatedAtAction(nameof(GetExercise), new { id = createdUserResult.Data!.Id }, createdUserResult.Data);
            
            return BadRequest(createdUserResult);
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

