using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpGet("Filter")]
    public async Task<IActionResult> GetUserFilter(string username)
    {
        var result = await _service.GetUserFilter(username);
        return StatusCode((int)result.StatusCode, result); 
    }
    
    [HttpGet("GetUser")]
    public async Task<IActionResult> GetUser()
    {
        var result = await _service.GetUsers();
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet("GetUserById")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var result = await _service.GetUserById(id);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPost("AddUser")]
    public async Task<IActionResult> AddUser([FromQuery]AddUserDto user)
    {
        var response = await _service.AddUser(user);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPut("UpdateUser")]
    public async Task<IActionResult> UpdateUser([FromQuery]AddUserDto user)
    {
        var result = await _service.UpdateUser(user);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpDelete("DeleteUser")]
    public async Task<IActionResult> DeleteUser([FromQuery]int id)
    {
        var result = await _service.DeleteUser(id);
        return StatusCode((int)result.StatusCode, result);
    }
}