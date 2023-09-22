using Domain.Entities;
using Domain.Wrapper;

namespace Infrastructure.Services.GroupMemberService;

public interface IGroupMemberService
{
    public Task<List<GetGroupMemberFullInfoDto>> GetGroupMemberFullInfo();
    public Task<Response<List<GetGroupMemberDto>>> GetGroupMember();
    public Task<Response<GetGroupMemberDto>> GetGroupMemberById(int id);
    public Task<Response<GetGroupMemberDto>> AddGroupMember(AddGroupMemberDto groupMember);
    public Task<Response<GetGroupMemberDto>> UpdateGroupMember(AddGroupMemberDto groupMember);
    public Task<Response<bool>> DeleteGroupMember(int id);
}