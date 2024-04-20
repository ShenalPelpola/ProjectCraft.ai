using MediatR;

namespace Application.Projects.Queries.GenerateProject;

public sealed record GenerateProjectQuery(
    string PromptText
) : IRequest;