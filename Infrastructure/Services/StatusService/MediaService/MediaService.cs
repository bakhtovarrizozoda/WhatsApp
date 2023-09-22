using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Infrastructure.Services.AttachmentService.FileService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

public class MediaService : IMediaService
{
    private readonly IWebHostEnvironment _environment;

    public MediaService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string> CreateMediaAsync(string folder, IFormFile media)
    {
        var path = Path.Combine(_environment.WebRootPath, folder, media.FileName);
        using (var stream = new FileStream(path, FileMode.Create))
        {
            await media.CopyToAsync(stream);
        }

        return media.FileName;
    }

    public bool DeleteMedia(string folder, string medianame)
    {
        var path = Path.Combine(_environment.WebRootPath, folder, medianame);
        if (File.Exists(path))
        {
            File.Delete(path);
            return true;
        }
        else
        {
            return false;
        }
    }
}
