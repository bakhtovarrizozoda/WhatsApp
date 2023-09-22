using Domain.Entities;
using Infrastructure.Services.StatusService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class StatusController : ControllerBase
{
    private readonly IStatusService _service;

    public StatusController(IStatusService service)
    {
        _service = service;
    }

    [HttpGet("GetStatusFullInfo")]
    public async Task<List<GetStatusFullInfoDto>> GetStatusFullInfo()
    {
        return await _service.GetStatusFullInfo();
    }
    
    [HttpGet("GetStatus")]
    public async Task<IActionResult> GetStatus()
    {
        var result = await _service.GetStatus();
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet("GetStatusById")]
    public async Task<IActionResult> GetStatusById(int id)
    {
        var result = await _service.GetStatusById(id);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPost("AddStatus")]
    public async Task<IActionResult> AddStatus([FromQuery]AddStatusDto status)
    {
        var result = await _service.AddStatus(status);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPut("UpdateStatus")]
    public async Task<IActionResult> UpdateStatus([FromQuery]AddStatusDto status)
    {
        var result = await _service.UpdateStatus(status);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpDelete("DeleteUser")]
    public async Task<IActionResult> DeleteStatus([FromQuery]int id)
    {
        var result = await _service.DeleteStatus(id);
        return StatusCode((int)result.StatusCode, result);
    }
    
}