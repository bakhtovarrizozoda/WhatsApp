using Domain.Entities;
using Domain.Wrapper;

namespace Infrastructure.Services.UserService;

public interface IUserService
{
    public Task<Response<List<GetUserDto>>> GetUserFilter(string username);
    public Task<Response<List<GetUserDto>>> GetUsers();
    public Task<Response<GetUserDto>> GetUserById(int id);
    public Task<Response<GetUserDto>> AddUser(AddUserDto user);
    public Task<Response<GetUserDto>> UpdateUser(AddUserDto user);
    public Task<Response<bool>> DeleteUser(int id);
}