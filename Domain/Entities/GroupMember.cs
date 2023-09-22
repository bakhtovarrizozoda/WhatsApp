using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class GroupMember
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("Group")]
    public int GroupId { get; set; }
    [ForeignKey("User")]
    public int UserId { get; set; }
    public Group Group { get; set; }
    public User User { get; set; }
}