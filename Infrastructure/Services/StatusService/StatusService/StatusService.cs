using System.Net;
using AutoMapper;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.StatusService;

public class StatusService : IStatusService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IMediaService _mediaService;

    public StatusService(DataContext context, IMapper mapper, IMediaService mediaService)
    {
        _context = context;
        _mapper = mapper;
        _mediaService = mediaService;
    }

    public async Task<List<GetStatusFullInfoDto>> GetStatusFullInfo()
    {
        var result = await
            (from s in _context.Status
                join u in _context.Users on s.UserId equals u.Id into statusuoser
                from su in statusuoser.DefaultIfEmpty()
                select new GetStatusFullInfoDto()
                {
                    Id = s.Id,
                    Media = s.Media,
                    UserName = su.UserName,
                    CountShower = s.CountShower,
                    CreateData = s.CreateData
                }).ToListAsync();
        return result;
    }
    
    public async Task<Response<List<GetStatusDto>>> GetStatus()
    {
        try
        {
            var model = _context.Status.ToList();
            var result = _mapper.Map<List<GetStatusDto>>(model);
            return new Response<List<GetStatusDto>>(result);
        }
        catch (Exception e)
        {
            return new Response<List<GetStatusDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
    public async Task<Response<GetStatusDto>> GetStatusById(int id)
    {
        try
        {
            var find = await _context.Status.FindAsync(id);
            if (find == null) return new Response<GetStatusDto>(new GetStatusDto());
            var result = _mapper.Map<GetStatusDto>(find);
            return new Response<GetStatusDto>(result);
        }
        catch (Exception e)
        {
            return new Response<GetStatusDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
    
    public async Task<Response<GetStatusDto>> AddStatus(AddStatusDto status)
    {
        try
        {
            var medianame = await _mediaService.CreateMediaAsync("images", status.Media);
            var mediaEntity = new Status()
            {
                Id = status.Id,
                UserId = status.UserId,
                CountShower = status.CountShower,
                CreateData = status.CreateData,
                Media = status.Media.FileName
            };

            _context.Status.Add(mediaEntity);
            await _context.SaveChangesAsync();
            var getStatusDto = _mapper.Map<GetStatusDto>(mediaEntity);
            return new Response<GetStatusDto>(getStatusDto);

        }
        catch (Exception e)
        {
            return new Response<GetStatusDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetStatusDto>> UpdateStatus(AddStatusDto status)
    {
        try
        {
            var existing = await _context.Status.FindAsync(status.Id);
            if (existing == null)
            {
                return null;
            }

            if (status.Media != null)
            {
                if (existing.Media != null)
                {
                    _mediaService.DeleteMedia("images", existing.Media);
                }
                
                var medianame = await _mediaService.CreateMediaAsync("images", status.Media);
                existing.Media = medianame;
            }

            existing.Id = status.Id;
            existing.UserId = status.UserId;
            existing.CountShower = status.CountShower;
            existing.CreateData = status.CreateData;
            existing.Media = status.Media.FileName;
                

            await _context.SaveChangesAsync();

            var getStatusDto = _mapper.Map<GetStatusDto>(existing);
            return new Response<GetStatusDto>(getStatusDto);

        }
        catch (Exception e)
        {
            return new Response<GetStatusDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteStatus(int id)
    {
        try
        {
            var find = await _context.Status.FindAsync(id);
            if (find == null) return new Response<bool>(false);
            _context.Status.Remove(find);
            await _context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}