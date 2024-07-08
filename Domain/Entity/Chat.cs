namespace Data.Models;

public class Chat
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CreatorId { get; set; }
    public User Creator { get; set; }
    public ICollection<Message> Messages { get; set; }
}