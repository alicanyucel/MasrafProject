using AutoMapper;
using GenericRepository;
using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ProjectCards.UpdateProjectCards;

internal sealed class UpdateProjectCardCommandHandler(IProjectCardRepository projectCardRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateProjectCardCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateProjectCardCommand request, CancellationToken cancellationToken)
    {
        ProjectCard? projectCard = await projectCardRepository.GetByExpressionWithTrackingAsync(P => P.Id == request.Id, cancellationToken);
        if (projectCard == null)
        {
            return Result<string>.Failure("Project Card bulunamadi.");
        }
        mapper.Map(request, projectCard);
        projectCardRepository.Update(projectCard);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Project Card güncellendi.";
    }
}