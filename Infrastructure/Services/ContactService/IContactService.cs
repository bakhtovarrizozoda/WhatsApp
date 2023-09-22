using Domain.Entities;
using Domain.Wrapper;

namespace Infrastructure.Services.ContactService;

public interface IContactService
{
    public Task<List<GetContactFullInfoDto>> GetContactFullInfo();
    public Task<Response<List<GetContactDto>>> GetContact();
    public Task<Response<GetContactDto>> GetContactById(int id);
    public Task<Response<GetContactDto>> AddContact(AddContactDto contact);
    public Task<Response<GetContactDto>> UpdateContact(AddContactDto contact);
    public Task<Response<bool>> DeleteContact(int id);
}