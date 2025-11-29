using AutoMapper;
using GenericRepository;
using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;
using MasrafProject.Application.Interfaces;

namespace MasrafProject.Application.Features.ProjectCards.CreateProjectCards;

internal sealed class CreateProjectCardComamndHandler(
    IProjectCardRepository projectCardRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ITenantProvider tenantProvider) : IRequestHandler<CreateProjectCardCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateProjectCardCommand request, CancellationToken cancellationToken)
    {
        ProjectCard projectCard = mapper.Map<ProjectCard>(request);
        projectCard.TenantId = tenantProvider.TenantId;
        await projectCardRepository.AddAsync(projectCard, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Project Card kaydý yapýldý";
    }
}
