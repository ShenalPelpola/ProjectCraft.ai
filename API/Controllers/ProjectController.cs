using Microsoft.AspNetCore.Mvc;
using Application.Projects.Queries.GenerateProject;

namespace API.Controllers;

public class ProjectController : BaseController
{
    [HttpGet]
    public async Task GetProject()
    {
        try
        {
            var query = new GenerateProjectQuery("Hello World");

            await Mediator.Send(query);
        }
        catch (Exception ex)
        {
            StatusCode(500, "Internal server error: " + ex.Message);
        }
    }
}
