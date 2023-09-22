using Domain.Entities;
using Infrastructure.Services.AttachmentService.AttachmentService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class AttachmentController : ControllerBase
{
    private readonly IAttachmentService _service;

    public AttachmentController(IAttachmentService service)
    {
        _service = service;
    }

    [HttpGet("GetAttachmentFullInfo")]
    public async Task<List<GetAttachmentFullInfoDto>> GetAttachmentFullInfo()
    {
        return await _service.GetAttachmentFullInfo();
    }
    
    [HttpGet("GetAttachment")]
    public async Task<IActionResult> GetAttachment()
    {
        var result = await _service.GetAttachment();
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet("GetAttachmentById")]
    public async Task<IActionResult> GetAttachmentById(int id)
    {
        var result = await _service.GetAttachmentById(id);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPost("AddAttachment")]
    public async Task<IActionResult> AddAttachment([FromQuery]AddAttachmentDto attachment)
    {
        var result = await _service.AddAttachment(attachment);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPut("UpdateAttachment")]
    public async Task<IActionResult> UpdateAttachment([FromQuery]AddAttachmentDto attachment)
    {
        var result = await _service.UpdateAttachment(attachment);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpDelete("DeleteAttachment")]
    public async Task<IActionResult> DeleteGroup([FromQuery]int id)
    {
        var result = await _service.DeleteAttachment(id);
        return StatusCode((int)result.StatusCode, result);
    }
}