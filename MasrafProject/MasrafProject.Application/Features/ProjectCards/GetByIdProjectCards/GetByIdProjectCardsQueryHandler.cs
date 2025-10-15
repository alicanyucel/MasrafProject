using AutoMapper;
using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ProjectCards.GetByIdProjectCards;

public sealed class GetByIdProjectCardQueryHandler : IRequestHandler<GetByIdProjectCardQuery, Result<ProjectCard>>
{
    private readonly IProjectCardRepository _projectCardRepository;
    private readonly IMapper _mapper;

    public GetByIdProjectCardQueryHandler(IProjectCardRepository projectCardRepository, IMapper mapper)
    {
       _projectCardRepository =projectCardRepository;
        _mapper = mapper;
    }

    public async Task<Result<ProjectCard>> Handle(GetByIdProjectCardQuery request, CancellationToken cancellationToken)
    {
        var projectCardEntity = await _projectCardRepository.GetByExpressionAsync(
            x => x.Id == request.Id && !x.IsDeleted,
            cancellationToken
        );

        if (projectCardEntity is null)
        return Result<ProjectCard>.Failure("Proje kartı bulunamadı veya silinmiş.");
        var projectCard = _mapper.Map<ProjectCard>(projectCardEntity);
        return Result<ProjectCard>.Succeed(projectCard);
    }
}