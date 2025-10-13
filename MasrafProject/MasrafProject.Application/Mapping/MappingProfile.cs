using AutoMapper;
using MasrafProject.Application.Dtos;
using MasrafProject.Application.Features.ApprovalStatuses.CreateApprovalStatuses;
using MasrafProject.Application.Features.ApprovalStatuses.UpdateApprovalStatuses;
using MasrafProject.Application.Features.Users.CreateUsers;
using MasrafProject.Application.Features.Users.UpdateUsers;
using MasrafProject.Domain.Entities;

namespace MasrafProject.Application.Mapping;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        //AprpovalStatus    
        CreateMap<CreateApprovalStatusCommand,ApprovalStatus>().ReverseMap();
        CreateMap<UpdateApprovalStatusCommand, ApprovalStatus>().ReverseMap();
        //User
        CreateMap<AppUser, UserDto>().ReverseMap();
        CreateMap<CreateUserCommand,AppUser>().ReverseMap();
        CreateMap<UpdateUserCommand,AppUser>().ReverseMap();
    }
}
