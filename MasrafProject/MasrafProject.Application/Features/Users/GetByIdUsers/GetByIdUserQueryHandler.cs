using AutoMapper;
using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.Users.GetByIdUsers;

public sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<AppUser>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<Result<AppUser>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var userEntity = await _userRepository.GetByExpressionAsync(
            x => x.Id == request.Id && !x.IsDeleted,
            cancellationToken
        );
        if (userEntity is null)
        return Result<AppUser>.Failure("Kullanıcı bulunamadı veya silinmiş.");
        var user = _mapper.Map<AppUser>(userEntity);
        return Result<AppUser>.Succeed(user);
    }
}