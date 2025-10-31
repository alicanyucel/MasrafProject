using AutoMapper;
using GenericRepository;
using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.Users.UpdateUsers;
internal sealed class UpdateUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper
) : IRequestHandler<UpdateUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        AppUser? user = await userRepository.GetByExpressionWithTrackingAsync(u => u.Id == request.Id, cancellationToken);
        if (user is null)
        {
            return Result<string>.Failure("Kullanıcı bulunamadı.");
        }
        mapper.Map(request, user);
        userRepository.Update(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Kullanıcı güncellendi.");
    }
}