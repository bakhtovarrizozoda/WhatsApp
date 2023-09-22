namespace Domain.Entities;

public class GetGroupFullInfoDto 
{
    public int Id { get; set; }
    public string GroupName { get; set; }
    public DateTime CreationDate { get; set; }
    public List<string> GroupMembersId { get; set; }
    public string UserName { get; set; }
}