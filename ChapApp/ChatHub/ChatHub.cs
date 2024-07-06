using Application.IServices;
using Microsoft.AspNetCore.SignalR;

namespace Presentation.ChatHub;

public class ChatHub : Hub
{
    private readonly IChatService _chatService;

    public ChatHub(IChatService chatService)
    {
        _chatService = chatService;
    }

    public async Task SendMessage(int chatId, int userId, string message)
    {
        var msg = await _chatService.SendMessageAsync(chatId, userId, message);
        await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", msg);
    }

    public async Task JoinChat(int chatId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
    }

    public async Task LeaveChat(int chatId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.ToString());
    }
}