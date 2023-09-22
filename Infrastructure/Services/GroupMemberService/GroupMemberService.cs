using System.Net;
using AutoMapper;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.GroupMemberService;

public class GroupMemberService : IGroupMemberService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public GroupMemberService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetGroupMemberFullInfoDto>> GetGroupMemberFullInfo()
    {
        var result = await (
            from gm in _context.GroupMembers
            join u in _context.Users on gm.UserId equals u.Id
            join g in _context.Groups on gm.GroupId equals g.Id into groupuser
            from gu in groupuser.DefaultIfEmpty()
            select new GetGroupMemberFullInfoDto()
            {
                Id = gm.Id,
                UserName = u.UserName,
                GroupName = gu.GroupName
            }
        ).ToListAsync();
        return result;
    }
    
    public async Task<Response<List<GetGroupMemberDto>>> GetGroupMember()
    {
        try
        {
            var model = _context.GroupMembers.ToList();
            var result = _mapper.Map<List<GetGroupMemberDto>>(model);
            return new Response<List<GetGroupMemberDto>>(result);
        }
        catch (Exception e)
        {
            return new Response<List<GetGroupMemberDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
    public async Task<Response<GetGroupMemberDto>> GetGroupMemberById(int id)
    {
        try
        {
            var find = await _context.GroupMembers.FindAsync(id);
            if (find == null) return new Response<GetGroupMemberDto>(new GetGroupMemberDto());
            var result = _mapper.Map<GetGroupMemberDto>(find);
            return new Response<GetGroupMemberDto>(result);
        }
        catch (Exception e)
        {
            return new Response<GetGroupMemberDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
    
    public async Task<Response<GetGroupMemberDto>> AddGroupMember(AddGroupMemberDto groupMember)
    {
        try
        {
            var model = _mapper.Map<GroupMember>(groupMember);
            await _context.GroupMembers.AddAsync(model);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<GetGroupMemberDto>(model);
            return new Response<GetGroupMemberDto>(result);
        }
        catch (Exception e)
        {
            return new Response<GetGroupMemberDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetGroupMemberDto>> UpdateGroupMember(AddGroupMemberDto groupMember)
    {
        try
        {
            var find = await _context.GroupMembers.FindAsync(groupMember.Id);
            _mapper.Map(groupMember, find);
            _context.Entry(find).State = EntityState.Modified;
            var result = _mapper.Map<GetGroupMemberDto>(find);
            return new Response<GetGroupMemberDto>(result);
        }
        catch (Exception e)
        {
            return new Response<GetGroupMemberDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteGroupMember(int id)
    {
        try
        {
            var find = await _context.GroupMembers.FindAsync(id);
            if (find == null) return new Response<bool>(false);
            _context.GroupMembers.Remove(find);
            await _context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}