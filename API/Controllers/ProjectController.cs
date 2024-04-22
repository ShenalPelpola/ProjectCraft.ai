using Microsoft.AspNetCore.Mvc;
using Domain.Models.Requests;
using Application.Projects.Queries.GenerateProject;
using Domain.Models;

namespace API.Controllers;

public class ProjectController : BaseController
{
    [HttpPost("generate")]
    public async Task<ProjectGeneration> GenerateProject([FromBody] ChatServiceRequest chatServiceRequest)
    {
        try
        {
            GenerateProjectQuery query = new GenerateProjectQuery(chatServiceRequest.ProjectId, chatServiceRequest.Prompt);

            return await Mediator.Send(query);
        }
        catch (Exception ex)
        {
            StatusCode(500, "Internal server error: " + ex.Message);
            return null;
        }
    }
}

