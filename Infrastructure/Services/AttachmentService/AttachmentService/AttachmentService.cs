using System.Net;
using AutoMapper;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.AttachmentService.AttachmentService;

public class AttachmentService : IAttachmentService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public AttachmentService(DataContext context, IMapper mapper, IFileService fileService)
    {
        _context = context;
        _mapper = mapper;
        _fileService = fileService;
    }

    public async Task<List<GetAttachmentFullInfoDto>> GetAttachmentFullInfo()
    {
        var result = await (
            from a in _context.Attachments
            join u in _context.Users on a.MessageId equals u.Id
            join m in _context.Messages on a.MessageId equals m.Id into attachuser
            from au in attachuser.DefaultIfEmpty()
            select new GetAttachmentFullInfoDto()
            {
                Id = a.Id,
                FileName = a.FileName,
                MessageText = au.MessageText,
                UserName = u.UserName
            }).ToListAsync();
        return result;
    }
    
    
    public async Task<Response<List<GetAttachmentDto>>> GetAttachment()
    {
        try
        {
            var model = _context.Attachments.ToList();
            var result = _mapper.Map<List<GetAttachmentDto>>(model);
            return new Response<List<GetAttachmentDto>>(result);
        }
        catch (Exception e)
        {
            return new Response<List<GetAttachmentDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
    public async Task<Response<GetAttachmentDto>> GetAttachmentById(int id)
    {
        try
        {
            var find = await _context.Attachments.FindAsync(id);
            if (find == null) return new Response<GetAttachmentDto>(new GetAttachmentDto());
            var result = _mapper.Map<GetAttachmentDto>(find);
            return new Response<GetAttachmentDto>(result);
        }
        catch (Exception e)
        {
            return new Response<GetAttachmentDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    

    public async Task<Response<GetAttachmentDto>> AddAttachment(AddAttachmentDto attachment)
    {
        try
        {
            var filename = await _fileService.CreateFileAsync("images", attachment.File);
            var attachmentEntity = new Attachment()
            {
                Id = attachment.Id,
                MessageId = attachment.MessageId,
                FileName = attachment.File.FileName
            };

            _context.Attachments.Add(attachmentEntity);
            await _context.SaveChangesAsync();

            var getAttachmentDto = _mapper.Map<GetAttachmentDto>(attachmentEntity);
            return new Response<GetAttachmentDto>(getAttachmentDto);
            
        }
        catch (Exception e)
        {
            return new Response<GetAttachmentDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }   

    public async Task<Response<GetAttachmentDto>> UpdateAttachment(AddAttachmentDto attachment)
    {
        try
        {
            var existing = await _context.Attachments.FindAsync(attachment.Id);
            if (existing == null)
            {
                return null;
            }

            if (attachment.File != null)
            {
                if (existing.FileName != null)
                {
                    _fileService.DeleteFile("images", existing.FileName);
                }
                
                var filename = await _fileService.CreateFileAsync("images", attachment.File);
                existing.FileName = filename;
            }

            existing.Id = attachment.Id;
            existing.MessageId = attachment.MessageId;
            existing.FileName = attachment.File.FileName;

            await _context.SaveChangesAsync();

            var getAttachmentDto = _mapper.Map<GetAttachmentDto>(existing);
            return new Response<GetAttachmentDto>(getAttachmentDto);

        }
        catch (Exception e)
        {
            return new Response<GetAttachmentDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteAttachment(int id)
    {
        try
        {
            var find = await _context.Attachments.FindAsync(id);
            if (find == null) return new Response<bool>(false);
            _context.Attachments.Remove(find);
            await _context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}