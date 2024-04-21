namespace Domain.Models.Requests;

public class ChatServiceRequest
{
    public string ProjectId { get; set; }   

    public string ConversationId { get; set; }

    public string Prompt { get; set; }
}
