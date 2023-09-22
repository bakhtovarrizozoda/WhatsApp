using Microsoft.AspNetCore.Http;

namespace Domain.Entities;

public class AddStatusDto : StatusBaseDto
{
    public IFormFile Media { get; set; }
}