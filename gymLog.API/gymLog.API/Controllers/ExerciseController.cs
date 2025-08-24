using gymLog.API.Model;
using gymLog.API.Model.DTO.ExerciseDto;
using gymLog.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gymLog.API.Controllers;

[Authorize]
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
    public async Task<ActionResult<IEnumerable<ExerciseDto>>> GetExercises(Guid workoutDayId)
    {
        var result = await _service.GetAllExercises(workoutDayId);
        if (result.IsSuccess) return Ok(result.Data);
        return BadRequest(result.Message);
    }

    [HttpPut("{exerciseId:guid}")]
    public async Task<IActionResult> PutExercise(Guid exerciseId, CreateExerciseDto createExerciseDto)
    {
        var result = await _service.UpdateExercise(exerciseId, createExerciseDto);
        if (result.IsSuccess) return Ok(result.Data);
        return BadRequest(result.Message);
    }

    [HttpPost]
    public async Task<ActionResult<ExerciseDto>> PostExercise( CreateExerciseDto createExerciseDto)
    {
        var result = await _service.CreateExercise(createExerciseDto);
        if (result.IsSuccess) return Ok(result.Data);
        return BadRequest(result.Message);
    }

    [HttpDelete("{exerciseId:guid}")]
    public async Task<IActionResult> DeleteExercise(Guid exerciseId)
    {
        var result = await _service.RemoveExercise(exerciseId);
        if (result.IsSuccess) return Ok(result.Data);
        return BadRequest(result.Message);
    }
}