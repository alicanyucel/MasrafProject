using AutoMapper;
using GenericRepository;
using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;
using MasrafProject.Application.Interfaces;

namespace MasrafProject.Application.Features.ProjectCards.UpdateProjectCards;

internal sealed class UpdateProjectCardCommandHandler(
    IProjectCardRepository projectCardRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ITenantProvider tenantProvider) : IRequestHandler<UpdateProjectCardCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateProjectCardCommand request, CancellationToken cancellationToken)
    {
        ProjectCard? projectCard = await projectCardRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.Id, cancellationToken);
        if (projectCard is null)
        {
            return Result<string>.Failure("Project Card bulunamadý.");
        }
        mapper.Map(request, projectCard);
        projectCard.TenantId = tenantProvider.TenantId;
        projectCardRepository.Update(projectCard);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Project Card güncellendi.");
    }
}
