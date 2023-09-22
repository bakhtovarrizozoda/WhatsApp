using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Status
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("User")]
    public int UserId { get; set; }
    public string Media { get; set; }
    [Required]
    public int CountShower { get; set; }
    [Required]
    public DateTime CreateData { get; set; }
    public User User { get; set; }
}