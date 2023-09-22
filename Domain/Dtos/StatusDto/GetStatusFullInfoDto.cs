namespace Domain.Entities;

public class GetStatusFullInfoDto
{
    public int Id { get; set; }
    public string Media { get; set; }
    public string UserName { get; set; }
    public int CountShower { get; set; }
    public DateTime CreateData { get; set; }
}