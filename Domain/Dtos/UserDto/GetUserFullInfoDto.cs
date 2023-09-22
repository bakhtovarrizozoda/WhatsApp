namespace Domain.Entities;

public class GetUserFullInfoDto : UserBaseDto
{
    public string GroupMembersName { get; set; }
    public string GroupsName { get; set; }
    public int Users { get; set; }
    public int ContactUsers { get; set; }
    public string StatusName { get; set; }
}