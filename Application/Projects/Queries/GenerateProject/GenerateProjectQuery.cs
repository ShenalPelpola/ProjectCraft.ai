using Domain.Models;
using MediatR;

namespace Application.Projects.Queries.GenerateProject;

public sealed record GenerateProjectQuery(
    string projectId,
    string ConversationId,
    string Prompt
) : IRequest<ProjectGeneration>;