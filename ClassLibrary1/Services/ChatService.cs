using Application.IServices;
using Data.DataBaseContext;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class ChatService : IChatService
{
    private readonly ChatAppDbContext _context;

    public ChatService(ChatAppDbContext context)
    {
        _context = context;
    }

    public async Task<Chat> CreateChatAsync(string name, int creatorId)
    {
        var chat = new Chat { Name = name, CreatorId = creatorId };
        _context.Chats.Add(chat);
        await _context.SaveChangesAsync();
        return chat;
    }

    public async Task<Chat> GetChatAsync(int chatId)
    {
        return await _context.Chats.Include(c => c.Messages).FirstOrDefaultAsync(c => c.Id == chatId);
    }

    public async Task<bool> DeleteChatAsync(int chatId, int userId)
    {
        var chat = await _context.Chats.FirstOrDefaultAsync(c => c.Id == chatId);
        if (chat == null || chat.CreatorId != userId)
        {
            return false;
        }

        _context.Chats.Remove(chat);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Message> SendMessageAsync(int chatId, int userId, string message)
    {
        var msg = new Message { ChatId = chatId, UserId = userId, Text = message, Timestamp = DateTime.UtcNow };
        _context.Messages.Add(msg);
        await _context.SaveChangesAsync();
        return msg;
    }

    public async Task<IEnumerable<Chat>> SearchChatsAsync(string searchTerm)
    {
        return await _context.Chats
            .Where(c => c.Name.Contains(searchTerm))
            .ToListAsync();
    }
}