using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Message
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("Sender")]
    public int SenderId { get; set; }
    [ForeignKey("Receiver")]
    public int ReceiverId { get; set; }
    [Required, MaxLength(250)]
    public string MessageText { get; set; }
    [Required]
    public DateTime SentDate { get; set; }
    [Required]
    public bool IsRead { get; set; }
    public User Sender { get; set; }
    public User Receiver { get; set; }
    public List<Attachment> Attachments { get; set; }
}