namespace Domain.Entities;

public class GetAttachmentFullInfoDto 
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string MessageText { get; set; }
    public string UserName { get; set;  }
}