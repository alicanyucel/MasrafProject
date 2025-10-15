using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace MasrafProject.Application.Features.ProjectCards.GetAllProjectCards;

internal sealed class GetAllProjectCardQueryHandler : IRequestHandler<GetAllProjectCardQuery, Result<List<ProjectCard>>>
{
    private readonly IProjectCardRepository _projectCardRepository;

    public GetAllProjectCardQueryHandler(IProjectCardRepository projectCardRepository)
    {
        _projectCardRepository = projectCardRepository;
    }

    public async Task<Result<List<ProjectCard>>> Handle(GetAllProjectCardQuery request, CancellationToken cancellationToken)
    {
        var projectCard = await _projectCardRepository
            .GetAll()
            .Where(x => !x.IsDeleted)
            .ToListAsync(cancellationToken);
        return Result<List<ProjectCard>>.Succeed(projectCard);
    }
}