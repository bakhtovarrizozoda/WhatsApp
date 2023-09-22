using Domain.Entities;
using Infrastructure.Services.ContactService;
using Infrastructure.Services.GroupService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class GroupController : ControllerBase
{
    private readonly IGroupService _service;

    public GroupController(IGroupService service)
    {
        _service = service;
    }

    [HttpGet("GroupFullInfo")]
    public async Task<List<GetGroupFullInfoDto>> GroupFullInfo()
    {
        return await _service.GroupFullInfo();
    }
    
    [HttpGet("GetGroup")]
    public async Task<IActionResult> GetGroup()
    {
        var result = await _service.GetGroup();
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet("GetGroupById")]
    public async Task<IActionResult> GetGroupById(int id)
    {
        var result = await _service.GetGroupById(id);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPost("AddGroup")]
    public async Task<IActionResult> AddGroup([FromQuery]AddGroupDto group)
    {
        var response = await _service.AddGroup(group);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPut("UpdateGroup")]
    public async Task<IActionResult> UpdateGroup([FromQuery]AddGroupDto group)
    {
        var result = await _service.UpdateGroup(group);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpDelete("DeleteGroup")]
    public async Task<IActionResult> DeleteGroup([FromQuery]int id)
    {
        var result = await _service.DeleteGroup(id);
        return StatusCode((int)result.StatusCode, result);
    }
}