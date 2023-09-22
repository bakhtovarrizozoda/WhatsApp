using Domain.Entities;
using Infrastructure.Services.MassageService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MessagaController : ControllerBase
{
    private readonly IMessageService _service;

    public MessagaController(IMessageService service)
    {
        _service = service;
    }

    [HttpGet("GetMessageFullInfo")]
    public async Task<List<GetMessageFullInfoDto>> GetMessageFullInfo()
    {
        return await _service.GetMessageFullInfo();
    }
    
    [HttpGet("GetMessage")]
    public async Task<IActionResult> GetMessage()
    {
        var result = await _service.GetMessage();
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet("GetMessageById")]
    public async Task<IActionResult> GetMessageById(int id)
    {
        var result = await _service.GetMessageById(id);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPost("AddMessage")]
    public async Task<IActionResult> AddMessage([FromQuery]AddMessageDto message)
    {
        var response = await _service.AddMessage(message);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPut("UpdateMessage")]
    public async Task<IActionResult> UpdateMessage([FromQuery]AddMessageDto message)
    {
        var result = await _service.UpdateMessage(message);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpDelete("DeleteMessage")]
    public async Task<IActionResult> DeleteMessage([FromQuery]int id)
    {
        var result = await _service.DeleteMessage(id);
        return StatusCode((int)result.StatusCode, result);
    }
}