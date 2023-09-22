namespace Domain.Entities;

public class GroupBaseDto
{
    public int Id { get; set; }
    public string GroupName { get; set; }
    public int CreatorId { get; set; }
    public DateTime CreationDate { get; set; }
}