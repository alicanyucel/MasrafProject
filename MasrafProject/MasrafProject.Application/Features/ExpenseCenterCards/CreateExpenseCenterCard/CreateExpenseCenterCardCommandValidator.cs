using FluentValidation;

namespace MasrafProject.Application.Features.ExpenseCenterCards.CreateExpenseCenterCard;

public class CreateExpenseCenterCardCommandValidator : AbstractValidator<CreateExpenseCenterCardCommand>
{
    public CreateExpenseCenterCardCommandValidator()
    {
        RuleFor(x => x.MasrafMerkeziKodu)
            .NotEmpty().MaximumLength(20);

        RuleFor(x => x.MasrafMerkeziAdi)
            .NotEmpty().MaximumLength(100);
    }
}
