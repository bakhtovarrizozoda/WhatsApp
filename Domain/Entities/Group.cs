using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Group
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(50)]
    public string GroupName { get; set; }
    [ForeignKey("Creator")]
    public int CreatorId { get; set; }
    [Required]
    public DateTime CreationDate { get; set; }
    public User Creator { get; set; }
    public List<GroupMember> GroupMembers { get; set; }
}