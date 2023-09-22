using  Microsoft.AspNetCore.Http;
public interface IMediaService
{
    Task<string> CreateMediaAsync(string folder,IFormFile media);
    bool DeleteMedia(string folder, string medianame);
}
