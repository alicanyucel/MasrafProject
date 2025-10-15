using FluentValidation;

namespace MasrafProject.Application.Features.ProjectCards.CreateProjectCards;

public class CreateProjectCardCommandValidator : AbstractValidator<CreateProjectCardCommand>
{
    public CreateProjectCardCommandValidator()
    {
        RuleFor(x => x.ProjeKodu)
            .NotEmpty().WithMessage("Proje kodu boş olamaz.")
            .MaximumLength(20).WithMessage("Proje kodu en fazla 20 karakter olabilir.");

        RuleFor(x => x.ProjeAdi)
            .NotEmpty().WithMessage("Proje adı boş olamaz.")
            .MaximumLength(150).WithMessage("Proje adı en fazla 150 karakter olabilir.");
    }
}
