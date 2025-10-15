using AutoMapper;
using GenericRepository;
using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ProjectCards.CreateProjectCards;

internal sealed class CreateProjectCardComamndHandler(IProjectCardRepository projectCardRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateProjectCardCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateProjectCardCommand request, CancellationToken cancellationToken)
    {
        ProjectCard projectCard = mapper.Map<ProjectCard>(request);
        await projectCardRepository.AddAsync(projectCard, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Project Card kaydı yapıldı";
    }
}
