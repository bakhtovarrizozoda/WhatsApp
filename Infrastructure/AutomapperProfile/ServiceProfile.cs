using AutoMapper;
using Domain.Entities;

namespace Infrastructure.AutomapperProfile;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<User, GetUserDto>().ReverseMap();
        CreateMap<AddUserDto, User>().ReverseMap();
        
        
        CreateMap<Status, GetStatusDto>().ReverseMap();
        CreateMap<AddStatusDto, Status>().ReverseMap();
        
        CreateMap<Message, GetMessageDto>().ReverseMap();
        CreateMap<AddMessageDto, Message>().ReverseMap();
        
        CreateMap<Group, GetGroupDto>().ReverseMap();
        CreateMap<AddGroupDto, Group>().ReverseMap();
        
        CreateMap<GroupMember, GetGroupMemberDto>().ReverseMap();
        CreateMap<AddGroupMemberDto, GroupMember>().ReverseMap();
        
        CreateMap<Contact, GetContactDto>().ReverseMap();
        CreateMap<AddContactDto, Contact>().ReverseMap();
        
        CreateMap<Attachment, GetAttachmentDto>().ReverseMap();
        CreateMap<AddAttachmentDto, Attachment>().ReverseMap();
        
        
    }
}