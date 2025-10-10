using AutoMapper;
using MasrafProject.Application.Features.ApprovalStatuses.CreateApprovalStatuses;
using MasrafProject.Domain.Entities;

namespace MasrafProject.Application.Mapping;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateApprovalStatusCommand,ApprovalStatus>().ReverseMap();
    }
}
