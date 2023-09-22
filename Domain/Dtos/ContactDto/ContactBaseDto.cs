namespace Domain.Entities;

public class ContactBaseDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ContactUserId { get; set; }
}