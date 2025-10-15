using GenericRepository;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ProjectCards.DeleteProjectCards;

public sealed class DeleteProjectCardCommandHandler : IRequestHandler<DeleteProjectCardCommand, Result<string>>
{
    private readonly IProjectCardRepository _projectCardRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProjectCardCommandHandler(IProjectCardRepository projectCardRepository , IUnitOfWork unitOfWork)
    {
        _projectCardRepository = projectCardRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteProjectCardCommand request, CancellationToken cancellationToken)
    {
        var projectCard = await _projectCardRepository.GetByExpressionAsync(
            x => x.Id == request.Id && !x.IsDeleted,
            cancellationToken
        );

        if (projectCard is null)
        return Result<string>.Failure("Proje kartı bulunamadı veya zaten silinmiş.");
        projectCard.IsDeleted = true;
        _projectCardRepository.Update(projectCard);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Proje kartı başarıyla silindi (soft delete).");
    }
}