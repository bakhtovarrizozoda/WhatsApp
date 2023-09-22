namespace Domain.Entities;

public class MessageBaseDto
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public string MessageText { get; set; }
    public DateTime SentDate { get; set; }
    public bool IsRead { get; set; }
}