using Domain.Entities;
using Domain.Wrapper;

namespace Infrastructure.Services.AttachmentService.AttachmentService;

public interface IAttachmentService
{
    public Task<List<GetAttachmentFullInfoDto>> GetAttachmentFullInfo();
    public Task<Response<List<GetAttachmentDto>>> GetAttachment();
    public Task<Response<GetAttachmentDto>> GetAttachmentById(int id);
    public Task<Response<GetAttachmentDto>> AddAttachment(AddAttachmentDto attachment);
    public Task<Response<GetAttachmentDto>> UpdateAttachment(AddAttachmentDto attachment);
    public Task<Response<bool>> DeleteAttachment(int id);
}