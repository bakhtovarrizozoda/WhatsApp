using System.Net;
using AutoMapper;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.MassageService;

public class MessageService : IMessageService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public MessageService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetMessageFullInfoDto>> GetMessageFullInfo()
    {
        var result = await
            (from m in _context.Messages
                join u in _context.Users on m.SenderId equals u.Id
                join us in _context.Users on m.ReceiverId equals us.Id into messageuser
                from mu in messageuser.DefaultIfEmpty()
                select new GetMessageFullInfoDto()
                {
                    Id = m.Id,
                    SenderName = u.UserName,
                    ReceiverName = mu.UserName,
                    MessageText = m.MessageText,
                    SentDate = m.SentDate,
                    IsRead = m.IsRead
                }).ToListAsync();
        return result;
    }
    
    public async Task<Response<List<GetMessageDto>>> GetMessage()
    {
        try
        {
            var model = _context.Messages.ToList();
            var result = _mapper.Map<List<GetMessageDto>>(model);
            return new Response<List<GetMessageDto>>(result);
        }
        catch (Exception e)
        {
            return new Response<List<GetMessageDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
    public async Task<Response<GetMessageDto>> GetMessageById(int id)
    {
        try
        {
            var find = await _context.Status.FindAsync(id);
            if (find == null) return new Response<GetMessageDto>(new GetMessageDto());
            var result = _mapper.Map<GetMessageDto>(find);
            return new Response<GetMessageDto>(result);
        }
        catch (Exception e)
        {
            return new Response<GetMessageDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
    
    public async Task<Response<GetMessageDto>> AddMessage(AddMessageDto message)
    {
        try
        {
            message.SentDate = DateTime.SpecifyKind(message.SentDate, DateTimeKind.Utc);
            message.SentDate = DateTime.UtcNow;
            var model = _mapper.Map<Message>(message);
            await _context.Messages.AddAsync(model);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<GetMessageDto>(model);
            return new Response<GetMessageDto>(result);
        }
        catch (Exception e)
        {
            return new Response<GetMessageDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetMessageDto>> UpdateMessage(AddMessageDto message)
    {
        try
        {
            var find = await _context.Messages.FindAsync(message.Id);
            _mapper.Map(message, find);
            _context.Entry(find).State = EntityState.Modified;
            var result = _mapper.Map<GetMessageDto>(find);
            return new Response<GetMessageDto>(result);
        }
        catch (Exception e)
        {
            return new Response<GetMessageDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteMessage(int id)
    {
        try
        {
            var find = await _context.Messages.FindAsync(id);
            if (find == null) return new Response<bool>(false);
            _context.Messages.Remove(find);
            await _context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}