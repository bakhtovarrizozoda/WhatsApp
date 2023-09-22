namespace Domain.Entities;

public class GetMessageFullInfoDto 
{
    public int Id { get; set; }
    public string SenderName { get; set; }
    public string ReceiverName { get; set; }
    public string MessageText { get; set; }
    public DateTime SentDate { get; set; }
    public bool IsRead { get; set; }
}