using gymLog.API.Model;
using gymLog.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace gymLog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ILogService _logService;
    private readonly IUserService _service;

    public UserController(IUserService userService, ILogService logService)
    {
        _service = userService;
        _logService = logService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Exercise>>> GetExercises()
    {
        _logService.LogInfo("Received request to get all exercises");
        try
        {
            var exercisesResult = await _service.GetAllAsync();
            if (exercisesResult.IsSuccess)
            {
                _logService.LogInfo("Successfully retrieved {count} exercises", exercisesResult.Data!.Count());
                return Ok(exercisesResult);
            }

            return BadRequest(exercisesResult.Message);
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, "Error occurred while getting all exercises");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Exercise>> GetExercise(Guid id)
    {
        _logService.LogInfo("Received request to get exercise with ID {id}", id);
        try
        {
            var userResult = await _service.GetByIdAsync(id);

            if (userResult.Data == null)
            {
                _logService.LogWarning("Exercise with ID {id} not found", id);
                return NotFound();
            }

            _logService.LogInfo("Successfully retrieved exercise with ID {id}", id);
            return Ok(userResult);
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, "Error occurred while getting exercise with ID {id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(Guid id, User user)
    {
        _logService.LogInfo("Received request to update user with ID {id}", id);
        try
        {
            if (id != user.Id)
            {
                _logService.LogWarning("User ID in URL ({urlId}) does not match user.Id ({userId}) in body", id,
                    user.Id);
                return BadRequest();
            }

            var updatedUserResult = await _service.UpdateAsync(user);
            _logService.LogInfo("Successfully updated user with ID {id}", id);
            return Ok(updatedUserResult);
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, "Error occurred while updating user with ID {id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<ActionResult<Exercise>> PostExercise(User user)
    {
        _logService.LogInfo("Received request to create a new user");
        try
        {
            var createdUserResult = await _service.CreateAsync(user);
            if (createdUserResult.IsSuccess)
            {
                _logService.LogInfo("Successfully created new user with ID {id}", createdUserResult.Data!.Id);
                return CreatedAtAction(nameof(GetExercise), new { id = createdUserResult.Data!.Id }, createdUserResult);
            }

            return BadRequest(createdUserResult.Message);
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, "Error occurred while creating a new user");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExercise(Guid id)
    {
        _logService.LogInfo("Received request to delete exercise with ID {id}", id);
        try
        {
            var result = await _service.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                _logService.LogWarning("Failed to delete exercise with ID {id}, not found", id);
                return NotFound();
            }

            _logService.LogInfo("Successfully deleted exercise with ID {id}", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, "Error occurred while deleting exercise with ID {id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}