using Domain.Entities;
using Infrastructure.Services.GroupMemberService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupMemberController : ControllerBase
{
    private readonly IGroupMemberService _service;

    public GroupMemberController(IGroupMemberService service)
    {
        _service = service;
    }

    [HttpGet("GetGroupMemberFull")]
    public async Task<List<GetGroupMemberFullInfoDto>> GetGroupMemberFullInfo()
    {
        return await _service.GetGroupMemberFullInfo();
    }
    
    [HttpGet("GetGroupMember")]
    public async Task<IActionResult> GetGroupMember()
    {
        var result = await _service.GetGroupMember();
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet("GetGroupMemberById")]
    public async Task<IActionResult> GetGroupMemberById(int id)
    {
        var result = await _service.GetGroupMemberById(id);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPost("AddGroupMember")]
    public async Task<IActionResult> AddGroupMember([FromQuery]AddGroupMemberDto groupMember)
    {
        var response = await _service.AddGroupMember(groupMember);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPut("UpdateGroupMember")]
    public async Task<IActionResult> UpdateGroupMember([FromQuery]AddGroupMemberDto groupMember)
    {
        var result = await _service.UpdateGroupMember(groupMember);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpDelete("DeleteGroupMember")]
    public async Task<IActionResult> DeleteGroupMember([FromQuery]int id)
    {
        var result = await _service.DeleteGroupMember(id);
        return StatusCode((int)result.StatusCode, result);
    }
}