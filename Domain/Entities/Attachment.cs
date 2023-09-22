using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;

namespace Domain.Entities;

public class Attachment
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("Message")]
    public int MessageId { get; set; }
    [Required, MaxLength(50)]
    public string FileName { get; set; }
    public Message Message { get; set; }
}