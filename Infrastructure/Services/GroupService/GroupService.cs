using System.Net;
using AutoMapper;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.GroupService;

public class GroupService : IGroupService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public GroupService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetGroupFullInfoDto>> GroupFullInfo()
    {
        var result = await
            (from g in _context.Groups
                join u in _context.Users on g.CreatorId equals u.Id into orderdetailgroup
                from odg in orderdetailgroup.DefaultIfEmpty()
                select new GetGroupFullInfoDto()
                {
                    Id = g.Id,
                    GroupName = g.GroupName,
                    UserName = odg.UserName,
                    CreationDate = g.CreationDate,
                    GroupMembersId = g.GroupMembers.Select(e => e.User.UserName).ToList()
                }).ToListAsync();
        return result;
    }

    public async Task<Response<List<GetGroupDto>>> GetGroup()
    {
        try
        {
            var model = _context.Groups.ToList();
            var result = _mapper.Map<List<GetGroupDto>>(model);
            return new Response<List<GetGroupDto>>(result);
        }
        catch (Exception e)
        {
            return new Response<List<GetGroupDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetGroupDto>> GetGroupById(int id)
    {
        try
        {
            var find = await _context.Groups.FindAsync(id);
            if (find == null) return new Response<GetGroupDto>(new GetGroupDto());
            var result = _mapper.Map<GetGroupDto>(find);
            return new Response<GetGroupDto>(result);
        }
        catch (Exception e)
        {
            return new Response<GetGroupDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetGroupDto>> AddGroup(AddGroupDto group)
    {
        try
        {
            group.CreationDate = DateTime.SpecifyKind(group.CreationDate, DateTimeKind.Utc);
            group.CreationDate = DateTime.UtcNow;
            var model = _mapper.Map<Group>(group);
            await _context.Groups.AddAsync(model);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<GetGroupDto>(model);
            return new Response<GetGroupDto>(result);
        }
        catch (Exception e)
        {
            return new Response<GetGroupDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetGroupDto>> UpdateGroup(AddGroupDto group)
    {
        try
        {
            var find = await _context.Groups.FindAsync(group.Id);
            _mapper.Map(group, find);
            _context.Entry(find).State = EntityState.Modified;
            var result = _mapper.Map<GetGroupDto>(find);
            return new Response<GetGroupDto>(result);
        }
        catch (Exception e)
        {
            return new Response<GetGroupDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteGroup(int id)
    {
        try
        {
            var find = await _context.Groups.FindAsync(id);
            if (find == null) return new Response<bool>(false);
            _context.Groups.Remove(find);
            await _context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}