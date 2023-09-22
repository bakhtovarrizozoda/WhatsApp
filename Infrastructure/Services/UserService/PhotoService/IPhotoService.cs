using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.UserService.PhotoService;

public interface IPhotoService
{
    Task<string> CreatePhotoAsync(string folder,IFormFile photo);
    bool DeletePhoto(string folder, string photoname);
}