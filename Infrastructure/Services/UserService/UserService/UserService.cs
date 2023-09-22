using System.Net;
using AutoMapper;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Context;
using Infrastructure.Services.UserService.PhotoService;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.UserService;

public class UserService : IUserService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IPhotoService _photoService;

    public UserService(DataContext context, IMapper mapper, IPhotoService photoService)
    {
        _context = context;
        _mapper = mapper;
        _photoService = photoService;
    }
    
    public async Task<Response<List<GetUserDto>>> GetUserFilter(string username)
    {
        try
        {
            var users = _context.Users
                .Where(u => u.UserName.ToLower().Contains(username.ToLower()))
                .ToList();
        
            var result = _mapper.Map<List<GetUserDto>>(users);
            return new Response<List<GetUserDto>>(result);
        }
        catch (Exception e)
        {
            return new Response<List<GetUserDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
    
    public async Task<Response<List<GetUserDto>>> GetUsers()
    {
        try
        {
            var model = _context.Users.ToList();
            var result = _mapper.Map<List<GetUserDto>>(model);
            return new Response<List<GetUserDto>>(result);
        }
        catch (Exception e)
        {
            return new Response<List<GetUserDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
    public async Task<Response<GetUserDto>> GetUserById(int id)
    {
        try
        {
            var find = await _context.Users.FindAsync(id);
            if (find == null) return new Response<GetUserDto>(new GetUserDto());
            var result = _mapper.Map<GetUserDto>(find);
            return new Response<GetUserDto>(result);
        }
        catch (Exception e)
        {
            return new Response<GetUserDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
    
    public async Task<Response<GetUserDto>> AddUser(AddUserDto user)
    {
        try
        {
            user.RegistrationDate = DateTime.SpecifyKind(user.RegistrationDate, DateTimeKind.Utc);
            user.RegistrationDate = DateTime.UtcNow;
            var medianame = await _photoService.CreatePhotoAsync("images", user.Photo);

            var model = new User()
            {
                Id = user.Id,
                UserName = user.UserName,
                PhoneNamber = user.PhoneNamber,
                Password = user.Password,
                RegistrationDate = user.RegistrationDate,
                Photo = user.Photo.FileName
            };
            await _context.Users.AddAsync(model);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<GetUserDto>(model);
            return new Response<GetUserDto>(result);
        }
        catch (Exception e)
        {
            return new Response<GetUserDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetUserDto>> UpdateUser(AddUserDto user)
    {
        try
        {
            user.RegistrationDate = DateTime.SpecifyKind(user.RegistrationDate, DateTimeKind.Utc);
            user.RegistrationDate = DateTime.UtcNow;
            var find = await _context.Users.FindAsync(user.Id);
            if (find == null)
            {
                return null;
            }

            if (user.Photo != null)
            {
                if (find.Photo != null)
                {
                    _photoService.DeletePhoto("images", find.Photo);
                }

                var photoname = await _photoService.CreatePhotoAsync("images", user.Photo);
                find.Photo = photoname;
            }

            find.Id = user.Id;
            find.UserName = user.UserName;
            find.PhoneNamber = user.PhoneNamber;
            find.Password = user.Password;
            find.RegistrationDate = user.RegistrationDate;
            find.Photo = user.Photo.FileName;

            await _context.SaveChangesAsync();
            var result = _mapper.Map<GetUserDto>(find);
            return new Response<GetUserDto>(result);
        }
        catch (Exception e)
        {
            return new Response<GetUserDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteUser(int id)
    {
        try
        {
            var find = await _context.Users.FindAsync(id);
            if (find == null) return new Response<bool>(false);
            _context.Users.Remove(find);
            await _context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}