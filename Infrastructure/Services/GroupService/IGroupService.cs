using Domain.Entities;
using Domain.Wrapper;

namespace Infrastructure.Services.GroupService;

public interface IGroupService
{
    public Task<List<GetGroupFullInfoDto>> GroupFullInfo();
    public Task<Response<List<GetGroupDto>>> GetGroup();
    public Task<Response<GetGroupDto>> GetGroupById(int id);
    public Task<Response<GetGroupDto>> AddGroup(AddGroupDto group);
    public Task<Response<GetGroupDto>> UpdateGroup(AddGroupDto group);
    public Task<Response<bool>> DeleteGroup(int id);
}