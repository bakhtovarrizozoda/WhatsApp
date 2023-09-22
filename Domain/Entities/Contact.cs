using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Contact
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("User")]
    public int UserId { get; set; }
    [ForeignKey("ContactUser")]
    public int ContactUserId { get; set; }
    public User User { get; set; }
    public User ContactUser { get; set; }
}