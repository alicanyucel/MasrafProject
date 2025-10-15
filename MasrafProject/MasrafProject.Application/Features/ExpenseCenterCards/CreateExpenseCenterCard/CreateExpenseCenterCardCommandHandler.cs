using AutoMapper;
using GenericRepository;
using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ExpenseCenterCards.CreateExpenseCenterCard;

internal sealed class CreateExpenseCenterCardComamndHandler(IExpenseCenterCardRepository customerRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateExpenseCenterCardCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateExpenseCenterCardCommand request, CancellationToken cancellationToken)
    {
        ExpenseCenterCard expenseCenterCard = mapper.Map<ExpenseCenterCard>(request);
        await customerRepository.AddAsync(expenseCenterCard, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Expense Center Card kaydı yapıldı";
    }
}