using FluentValidation;

namespace MasrafProject.Application.Features.Companies.CreateCompany;

public sealed class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        RuleFor(x => x.TenantId)
            .GreaterThan(0).WithMessage("TenantId sıfırdan büyük olmalıdır");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("İsim boş olamaz")
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Email alanı boş bırakılamaz")
            .EmailAddress().WithMessage("Lütfen geçerli bir email adresi giriniz")
            .MaximumLength(250).WithMessage("Email en fazla 250 karakter olabilir");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Telefon numarası boş olamaz")
            .Matches(@"^\d{10,15}$").WithMessage("Geçerli bir telefon numarası giriniz");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Adres boş olamaz")
            .MaximumLength(500);
    }
}
