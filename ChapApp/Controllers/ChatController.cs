using Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateChat([FromBody] CreateChatRequest request)
    {
        var chat = await _chatService.CreateChatAsync(request.Name, request.CreatorId);
        return CreatedAtAction(nameof(GetChat), new { id = chat.Id }, chat);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetChat(int id)
    {
        var chat = await _chatService.GetChatAsync(id);
        if (chat == null) return NotFound();
        return Ok(chat);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteChat(int id, [FromBody] DeleteChatRequest request)
    {
        var success = await _chatService.DeleteChatAsync(id, request.UserId);
        if (!success) return Forbid("There are no permissions to do the operation");
        return NoContent();
    }

    [HttpPost("{id}/messages")]
    public async Task<IActionResult> SendMessage(int id, [FromBody] SendMessageRequest request)
    {
        var message = await _chatService.SendMessageAsync(id, request.UserId, request.Text);
        return Ok(message);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchChats([FromQuery] string searchTerm)
    {
        var chats = await _chatService.SearchChatsAsync(searchTerm);
        return Ok(chats);
    }
}

public class CreateChatRequest
{
    public string Name { get; set; }
    public int CreatorId { get; set; }
}

public class DeleteChatRequest
{
    public int UserId { get; set; }
}

public class SendMessageRequest
{
    public int UserId { get; set; }
    public string Text { get; set; }
}