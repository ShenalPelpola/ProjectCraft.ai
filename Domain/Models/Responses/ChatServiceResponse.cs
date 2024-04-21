namespace Domain.Models.Responses;

public class ChatServiceResponse
{
    public string ConversationId { get; set; }
    public List<string> Commands { get; set; }
}