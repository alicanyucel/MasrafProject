using AutoMapper;
using MasrafProject.Application.Features.ApprovalStatuses.CreateApprovalStatuses;
using MasrafProject.Application.Features.ApprovalStatuses.UpdateApprovalStatuses;
using MasrafProject.Domain.Entities;

namespace MasrafProject.Application.Mapping;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        //AprpovalStatus    
        CreateMap<CreateApprovalStatusCommand,ApprovalStatus>().ReverseMap();
        CreateMap<UpdateApprovalStatusCommand, ApprovalStatus>().ReverseMap();
    }
}
