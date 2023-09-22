namespace Domain.Entities;

public class GroupMemberBaseDto
{
    public int Id { get; set; }
    public int GroupId { get; set; }
    public int UserId { get; set; }
}