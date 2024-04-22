
using System.Net.Http.Json;
using Domain.Models.Responses;

namespace Infrastucture.Services;

public class ChatService
{
    private readonly HttpClient _httpClient;

    public ChatService(HttpClient httpClient) 
    {
        _httpClient = httpClient;
    }

    public async Task<ChatServiceResponse> GetCommands(string conversationId, string prompt)
    {
        Dictionary<string, string> requestBody = new Dictionary<string, string>
        {
            { "prompt", prompt },
            { "conversationId", conversationId }
        };

        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/chat/commands", requestBody);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ChatServiceResponse>() ?? new ChatServiceResponse();
        }
        else
        {
            return new ChatServiceResponse();
        }
    }
}