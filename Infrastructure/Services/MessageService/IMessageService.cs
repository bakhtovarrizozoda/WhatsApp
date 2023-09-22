using Domain.Entities;
using Domain.Wrapper;

namespace Infrastructure.Services.MassageService;

public interface IMessageService
{
    public Task<List<GetMessageFullInfoDto>> GetMessageFullInfo();
    public Task<Response<List<GetMessageDto>>> GetMessage();
    public Task<Response<GetMessageDto>> GetMessageById(int id);
    public Task<Response<GetMessageDto>> AddMessage(AddMessageDto message);
    public Task<Response<GetMessageDto>> UpdateMessage(AddMessageDto message);
    public Task<Response<bool>> DeleteMessage(int id);
}