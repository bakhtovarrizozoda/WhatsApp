using Domain.Entities;
using Infrastructure.Services.ContactService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactController : ControllerBase
{
    private readonly IContactService _service;

    public ContactController(IContactService service)
    {
        _service = service;
    }

    [HttpGet("GetContactFull")]
    public async Task<List<GetContactFullInfoDto>> GetContactFullInfo()
    {
        return await _service.GetContactFullInfo();
    }
    
    [HttpGet("GetContact")]
    public async Task<IActionResult> GetContact()
    {
        var result = await _service.GetContact();
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet("GetContactById")]
    public async Task<IActionResult> GetContactById(int id)
    {
        var result = await _service.GetContactById(id);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPost("AddContact")]
    public async Task<IActionResult> AddContact([FromQuery]AddContactDto contact)
    {
        var response = await _service.AddContact(contact);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPut("UpdateContact")]
    public async Task<IActionResult> UpdateContact([FromQuery]AddContactDto contact)
    {
        var result = await _service.UpdateContact(contact);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpDelete("DeleteContact")]
    public async Task<IActionResult> DeleteContact([FromQuery]int id)
    {
        var result = await _service.DeleteContact(id);
        return StatusCode((int)result.StatusCode, result);
    }
}