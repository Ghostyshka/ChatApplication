using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.DataBaseContext;

public class ChatAppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }

    public ChatAppDbContext(DbContextOptions<ChatAppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Chats)
            .WithOne(c => c.Creator)
            .HasForeignKey(c => c.CreatorId);

        modelBuilder.Entity<Chat>()
            .HasMany(c => c.Messages)
            .WithOne(m => m.Chat)
            .HasForeignKey(m => m.ChatId);
    }
}