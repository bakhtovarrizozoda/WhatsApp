using System.Net;
using AutoMapper;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.ContactService;

public class ContactService : IContactService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public ContactService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetContactFullInfoDto>> GetContactFullInfo()
    {
        var result = await (
            from c in _context.Contacts
            join u in _context.Users on c.UserId equals u.Id into contactuser
            from cu in contactuser.DefaultIfEmpty()
            select new GetContactFullInfoDto()
            {
                Id = c.Id,
                UserName = cu.UserName,
                ContactUserId = c.ContactUserId
            }).ToListAsync();
        return result;
    }
  
    public async Task<Response<List<GetContactDto>>> GetContact()
    {
        try
        {
            var model = _context.Contacts.ToList();
            var result = _mapper.Map<List<GetContactDto>>(model);
            return new Response<List<GetContactDto>>(result);
        }
        catch (Exception e)
        {
            return new Response<List<GetContactDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
    public async Task<Response<GetContactDto>> GetContactById(int id)
    {
        try
        {
            var find = await _context.Contacts.FindAsync(id);
            if (find == null) return new Response<GetContactDto>(new GetContactDto());
            var result = _mapper.Map<GetContactDto>(find);
            return new Response<GetContactDto>(result);
        }
        catch (Exception e)
        {
            return new Response<GetContactDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
    
    public async Task<Response<GetContactDto>> AddContact(AddContactDto contact)
    {
        try
        {
            var model = _mapper.Map<Contact>(contact);
            await _context.Contacts.AddAsync(model);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<GetContactDto>(model);
            return new Response<GetContactDto>(result);
        }
        catch (Exception e)
        {
            return new Response<GetContactDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetContactDto>> UpdateContact(AddContactDto contact)
    {
        try
        {
            var find = await _context.Contacts.FindAsync(contact.Id);
            _mapper.Map(contact, find);
            _context.Entry(find).State = EntityState.Modified;
            var result = _mapper.Map<GetContactDto>(find);
            return new Response<GetContactDto>(result);
        }
        catch (Exception e)
        {
            return new Response<GetContactDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteContact(int id)
    {
        try
        {
            var find = await _context.Contacts.FindAsync(id);
            if (find == null) return new Response<bool>(false);
            _context.Contacts.Remove(find);
            await _context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}