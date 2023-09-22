using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<GroupMember> GroupMembers { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<Status> Status { get; set; }


}