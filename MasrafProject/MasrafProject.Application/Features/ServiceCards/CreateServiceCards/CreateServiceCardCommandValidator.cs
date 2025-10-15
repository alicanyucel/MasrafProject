using FluentValidation;

namespace MasrafProject.Application.Features.ServiceCards.CreateServiceCards;

public class CreateServiceCardCommandValidator : AbstractValidator<CreateServiceCardCommand>
{
    public CreateServiceCardCommandValidator()
    {
        RuleFor(x => x.HizmetKodu)
            .NotEmpty().MaximumLength(20);

        RuleFor(x => x.HizmetAdi)
            .NotEmpty().MaximumLength(100);

        RuleFor(x => x.KdvOrani)
            .InclusiveBetween(0, 100).WithMessage("KDV oranı 0 ile 100 arasında olmalıdır.");
    }
}
