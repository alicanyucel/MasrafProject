using GenericRepository;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.Users.DeleteUsers;

public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByExpressionAsync(
            x => x.Id == request.Id && !x.IsDeleted,
            cancellationToken
        );

        if (user is null)
        return Result<string>.Failure("Kullanıcı bulunamadı veya zaten silinmiş.");
        user.IsDeleted = true;
        _userRepository.Update(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Kullanıcı başarıyla silindi (soft delete).");
    }
}