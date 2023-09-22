using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.UserService.PhotoService;

public class PhotoService : IPhotoService
{
    private readonly IWebHostEnvironment _environment;

    public PhotoService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }
    public async Task<string> CreatePhotoAsync(string folder, IFormFile photo)
    {
        var path = Path.Combine(_environment.WebRootPath, folder, photo.FileName);
        using (var stream = new FileStream(path, FileMode.Create))
        {
            await photo.CopyToAsync(stream);
        }

        return photo.FileName;
    }

    public bool DeletePhoto(string folder, string photoname)
    {
        var path = Path.Combine(_environment.WebRootPath, folder, photoname);
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