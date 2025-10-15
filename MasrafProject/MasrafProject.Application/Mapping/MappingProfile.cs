using AutoMapper;
using MasrafProject.Application.Dtos;
using MasrafProject.Application.Features.ApprovalStatuses.CreateApprovalStatuses;
using MasrafProject.Application.Features.ApprovalStatuses.UpdateApprovalStatuses;
using MasrafProject.Application.Features.ExpenseCenterCards.CreateExpenseCenterCard;
using MasrafProject.Application.Features.ExpenseCenterCards.UpdateCenterCard;
using MasrafProject.Application.Features.Expenses.CreateExpense;
using MasrafProject.Application.Features.Expenses.UpdateExpense;
using MasrafProject.Application.Features.ProjectCards.CreateProjectCards;
using MasrafProject.Application.Features.ProjectCards.UpdateProjectCards;
using MasrafProject.Application.Features.ServiceCards.CreateServiceCards;
using MasrafProject.Application.Features.ServiceCards.UpdateServiceCards;
using MasrafProject.Application.Features.Users.CreateUsers;
using MasrafProject.Application.Features.Users.UpdateUsers;
using MasrafProject.Domain.Entities;

namespace MasrafProject.Application.Mapping;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Service Card
        CreateMap<UpdateServiceCardCommand, ServiceCard>().ReverseMap();
        CreateMap<CreateServiceCardCommand, ServiceCard>().ReverseMap();
        //Expense Center Card
        CreateMap<CreateExpenseCenterCardCommand, ExpenseCenterCard>().ReverseMap();    
        CreateMap<UpdateExpenseCenterCardCommand, ExpenseCenterCard>().ReverseMap();
        //project cards
        CreateMap<CreateProjectCardCommand, ProjectCard>().ReverseMap();
        CreateMap<UpdateProjectCardCommand, ProjectCard>().ReverseMap();
        //Expense
        CreateMap<UpdateExpenseCommand, Expense>().ReverseMap();
        CreateMap<CreateExpenseCommand, Expense>().ReverseMap();
        //AprpovalStatus    
        CreateMap<CreateApprovalStatusCommand,ApprovalStatus>().ReverseMap();
        CreateMap<UpdateApprovalStatusCommand, ApprovalStatus>().ReverseMap();
        //User
        CreateMap<AppUser, UserDto>().ReverseMap();
        CreateMap<CreateUserCommand,AppUser>().ReverseMap();
        CreateMap<UpdateUserCommand,AppUser>().ReverseMap();
    }
}
