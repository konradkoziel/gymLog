using gymLog.API.Model;
using gymLog.API.Model.DTO.WorkoutDayDto;
using gymLog.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gymLog.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class WorkoutDayController : ControllerBase
{
    private readonly IWorkoutDayService _service;

    public WorkoutDayController(IWorkoutDayService workoutDayService)
    {
        _service = workoutDayService;
    }

    [HttpGet("all/{workoutPlanId:guid}")]
    public async Task<ActionResult<IEnumerable<WorkoutDayDto>>> GetWorkoutDays(Guid workoutPlanId)
    {
        var result = await _service.GetAllWorkoutDays(workoutPlanId);
        if (result.IsSuccess) return Ok(result.Data);
        return BadRequest(result.Message);
    }

    [HttpGet("{workoutDayId:guid}")]
    public async Task<ActionResult<WorkoutDayDto>> GetWorkoutDay(Guid workoutDayId)
    {
        var result = await _service.GetWorkoutDayById(workoutDayId);
        if (result.IsSuccess) return Ok(result.Data);
        return BadRequest(result.Message);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutWorkoutDay(Guid workoutDayId, CreateWorkoutDayDto workoutDayDto)
    {
        var result = await _service.UpdateWorkoutDay(workoutDayId, workoutDayDto);
        if (result.IsSuccess) return Ok(result.Data);
        return BadRequest(result.Message);
    }

    [HttpPost("{workoutPlanId:guid}")]
    public async Task<ActionResult<WorkoutDay>> PostWorkoutDay(Guid workoutPlanId, CreateWorkoutDayDto workoutDayDto)
    {
        var result = await _service.CreateWorkoutDay(workoutPlanId, workoutDayDto);
        if (result.IsSuccess) return Ok(result.Data);
        return BadRequest(result.Message);
    }

    [HttpDelete("{workoutDayId:guid}")]
    public async Task<IActionResult> DeleteWorkoutDay(Guid workoutDayId)
    {
        var result = await _service.RemoveWorkoutDay(workoutDayId);
        if (result.IsSuccess) return Ok(result.Data);
        return BadRequest(result.Message);
    }
}