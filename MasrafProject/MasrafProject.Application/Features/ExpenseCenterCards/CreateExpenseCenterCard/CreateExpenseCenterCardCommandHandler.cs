using AutoMapper;
using GenericRepository;
using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;
using MasrafProject.Application.Interfaces;

namespace MasrafProject.Application.Features.ExpenseCenterCards.CreateExpenseCenterCard;

internal sealed class CreateExpenseCenterCardComamndHandler(
    IExpenseCenterCardRepository customerRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ITenantProvider tenantProvider) : IRequestHandler<CreateExpenseCenterCardCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateExpenseCenterCardCommand request, CancellationToken cancellationToken)
    {
        ExpenseCenterCard expenseCenterCard = mapper.Map<ExpenseCenterCard>(request);
        expenseCenterCard.TenantId = tenantProvider.TenantId;
        await customerRepository.AddAsync(expenseCenterCard, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Expense Center Card kaydý yapýldý";
    }
}
