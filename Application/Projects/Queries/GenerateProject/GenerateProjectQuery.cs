using Domain.Models;
using MediatR;

namespace Application.Projects.Queries.GenerateProject;

public sealed record GenerateProjectQuery(
    string ProjectId,
    string Prompt
) : IRequest<ProjectGeneration>;