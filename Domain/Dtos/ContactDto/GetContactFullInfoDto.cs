namespace Domain.Entities;

public class GetContactFullInfoDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public int ContactUserId { get; set; }
}