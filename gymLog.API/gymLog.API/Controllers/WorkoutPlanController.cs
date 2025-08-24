using gymLog.API.Model;
using gymLog.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gymLog.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class WorkoutPlanController : ControllerBase
{
    private readonly ILogService _logService;
    private readonly IWorkoutPlanService _service;

    public WorkoutPlanController(IWorkoutPlanService workoutPlanService, ILogService logService)
    {
        _service = workoutPlanService;
        _logService = logService;
    }

    [HttpGet("all/{userId:guid}")]
    public async Task<ActionResult<IEnumerable<PlanDto>>> GetWorkoutPlans(Guid userId)
    {
        var result = await _service.GetAllWorkoutPlans(userId);
        if (result.IsSuccess) return Ok(result.Data);
        return BadRequest(result.Message);
    }

    [HttpGet("{workoutPlanId:guid}")]
    public async Task<ActionResult<PlanDto>> GetWorkoutPlan(Guid workoutPlanId)
    {
        var result = await _service.GetWorkoutPlanById(workoutPlanId);
        if (result.IsSuccess) return Ok(result.Data);
        return BadRequest(result.Message);
    }

    [HttpPut("{workoutPlanId:guid}")]
    public async Task<IActionResult> PutWorkoutPlan(Guid workoutPlanId, CreatePlanDto workoutPlanDto)
    {
        var result = await _service.UpdateWorkoutPlan(workoutPlanId, workoutPlanDto);
        if (result.IsSuccess) return Ok(result.Data);
        return BadRequest(result.Message);
    }

    [HttpPost("{workoutPlanId:guid}")]
    public async Task<ActionResult<CreatePlanDto>> PostWorkoutPlan(Guid userId, CreatePlanDto workoutPlanDto)
    {
        var result = await _service.CreateWorkoutPlan(userId, workoutPlanDto);
        if (result.IsSuccess) return Ok(result.Data);
        return BadRequest(result.Message);
    }

    [HttpDelete("{workoutPlanId:guid}")]
    public async Task<IActionResult> DeleteWorkoutPlan(Guid workoutPlanId)
    {
        var result = await _service.RemoveWorkoutPlan(workoutPlanId);
        if (result.IsSuccess) return Ok(result.Data);
        return BadRequest(result.Message);
    }
}