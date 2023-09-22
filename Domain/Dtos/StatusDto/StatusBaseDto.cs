namespace Domain.Entities;

public class StatusBaseDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CountShower { get; set; }
    public DateTime CreateData { get; set; }
}