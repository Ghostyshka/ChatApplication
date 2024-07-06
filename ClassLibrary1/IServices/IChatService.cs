using Data.Models;

namespace Application.IServices;

public interface IChatService
{
    Task<Chat> CreateChatAsync(string name, int creatorId);
    Task<Chat> GetChatAsync(int chatId);
    Task<bool> DeleteChatAsync(int chatId, int userId);
    Task<Message> SendMessageAsync(int chatId, int userId, string message);
    Task<IEnumerable<Chat>> SearchChatsAsync(string searchTerm);
}