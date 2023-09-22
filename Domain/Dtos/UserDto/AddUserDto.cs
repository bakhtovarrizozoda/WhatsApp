using Microsoft.AspNetCore.Http;

namespace Domain.Entities;

public class AddUserDto : UserBaseDto
{
    public IFormFile Photo { get; set; }
}