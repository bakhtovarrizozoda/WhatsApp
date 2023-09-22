using Microsoft.AspNetCore.Http;

namespace Domain.Entities;

public class AddAttachmentDto : AttachmentBaseDto
{
    public IFormFile File { get; set; }
}
