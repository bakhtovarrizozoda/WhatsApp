using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class User
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(50)]
    public string UserName { get; set; }
    [Required, MaxLength(20)]
    public string PhoneNamber { get; set; }
    public string Photo { get; set; }
    [Required, MaxLength(20)]
    public string Password { get; set; }
    [Required]
    public DateTime RegistrationDate { get; set; }

    public List<GroupMember> GroupMembers { get; set; }
    [InverseProperty("Sender")] public List<Message> Senders { get; set; }
    [InverseProperty("Receiver")] public List<Message> Receivers { get; set; }
    public List<Group> Groups { get; set; }
    [InverseProperty("User")] public List<Contact> Users { get; set; }
    [InverseProperty("ContactUser")] public List<Contact> ContactUsers { get; set; }
    public List<Status> Status { get; set; }
}